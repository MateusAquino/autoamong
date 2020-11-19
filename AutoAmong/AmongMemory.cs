using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoAmongUs;

namespace AutoAmong
{
    class AmongMemory
    {
        public static void StartAnalyzing(Form2 form, Connection conn)
        {
            stop = false;
            if (init()) return;

            Console.WriteLine("======= AutoAmong started =======");
            analyze(form, conn);
        }

        static bool stop = false;
        public static void Stop() {
            oldPlayers = new List<Player>();
            stop = true;
        }

        public static bool init() {
            if (!AmongUsData.Init()) {
                Console.WriteLine("Não foi possível localizar o processo do jogo");
                MessageBox.Show("Abra o Among Us antes de iniciar o AutoAmong!");
                Form1.form.Show();
                Form2.exitOnClose = false;
                Form2.form.Close();
                return true;
            }
            return false;
        }

        static List<Player> oldPlayers = new List<Player>();
        static int updateDiscordWhen20 = 0;
        static bool startedOld, started = false;
        static bool startedNow = false;

        public static ClientState clientState;

        public static async void analyze(Form2 autoAmongForm, Connection conn) {
            while (!stop) {
                await Task.Delay(100);
                if (Form2.form.IsDisposed) break;
                if (updateDiscordWhen20++>=20) {
                    autoAmongForm.refreshDiscord();
                    updateDiscordWhen20 = 0;
                } if (startedOld != started) {
                    startedOld = started;
                    if (started) conn.startGame();
                    else conn.endGame();
                }

                if (init()) return;
                clientState = AmongUsData.GetClientState();
                bool shouldSkip = false;
                bool shouldContinue = false;

                switch (clientState)
                {
                    case ClientState.MainMenu:
                        autoAmongForm.setEstado(true, "No menu principal...");
                        autoAmongForm.setEstado(false, "...");
                        autoAmongForm.dgRemoveAll();
                        if (autoAmongForm.endAmong())
                            conn.endGame();
                        oldPlayers = new List<Player>();
                        shouldContinue = true;
                        break;
                    case ClientState.InLobby:
                        if (!autoAmongForm.getEstado(true).EndsWith("Aguardando no lobby..."))
                            conn.inGame(false);
                        autoAmongForm.setEstado(true, "Aguardando no lobby...");
                        autoAmongForm.setEstado(false, "...");
                        autoAmongForm.startAmong();
                        shouldSkip = true;
                        startedNow = true;
                        started = false;
                        break;
                    case ClientState.Playing:
                        if (!autoAmongForm.getEstado(true).EndsWith("Em jogo."))
                            conn.inGame(true);
                        autoAmongForm.setEstado(true, "Em jogo.");
                        started = true;
                        break;
                    case ClientState.EndGameMenu:
                        if (!autoAmongForm.getEstado(true).EndsWith("Fim de jogo.")) {
                            conn.endGame();
                            started = false;
                        }
                        autoAmongForm.setEstado(true, "Fim de jogo.");
                        started = false;
                        break;
                }

                if (shouldContinue) continue;

                MatchState matchState = AmongUsData.GetMatchState();
                if (!shouldSkip)
                    switch (matchState)
                    {
                        case MatchState.Discussion:
                            if (!autoAmongForm.getEstado(false).EndsWith("Em discussão."))
                                conn.inGame(false);
                            autoAmongForm.setEstado(false, "Em discussão.");
                            startedNow = false;
                            break;
                        case MatchState.NotVoted:
                            if (!autoAmongForm.getEstado(false).EndsWith("Em votação."))
                                conn.inGame(false);
                            autoAmongForm.setEstado(false, "Em votação.");
                            startedNow = false;
                            break;
                        case MatchState.Voted:
                            if (!autoAmongForm.getEstado(false).EndsWith("Em votação."))
                                conn.inGame(false);
                            autoAmongForm.setEstado(false, "Em votação.");
                            startedNow = false;
                            break;
                        case MatchState.DiscussionEnded:
                            bool delayAction = false;
                            if (!autoAmongForm.getEstado(false).EndsWith("Fim da discussão."))
                                delayAction = true;
                            autoAmongForm.setEstado(false, "Fim da discussão.");
                            int delay = 14000;
                            int interval = delay / 30;
                            if (!startedNow)
                                for (int i = 0; i < 30; i++)
                                {
                                    if (started && delayAction) Console.WriteLine("D:" + AmongUsData.GetClientState());
                                    if (started && delayAction && AmongUsData.GetClientState() == ClientState.Playing)
                                        await Task.Delay(interval);
                                }

                            if (AmongUsData.GetClientState() == ClientState.Playing)
                                if (started && delayAction)
                                {
                                    updatePlayers(autoAmongForm, conn);
                                    conn.inGame(true);
                                }
                            break;
                        case MatchState.NormalGame:
                            if (!autoAmongForm.getEstado(false).EndsWith("Jogando."))
                                conn.inGame(true);
                            autoAmongForm.setEstado(false, "Jogando.");
                            break;
                        default:
                            autoAmongForm.setEstado(false, "Jogando (aguardando discussão).");
                            break;

                    }
                updatePlayers(autoAmongForm, conn);
            }
        }

        public static void updatePlayers(Form2 autoAmongForm, Connection conn) {
            int index;
            List<Player> players = AmongUsData.GetPlayers();
            foreach (Player player in players)
                if ((index = oldPlayers.FindIndex(r => r.name.Equals(player.name))) == -1) // Entrou
                    autoAmongForm.dgAdd(player.name, player.color, "<Unlinked>");
                else
                { // Check alterações
                    if (oldPlayers[index].color != player.color) // Mudou cor
                        autoAmongForm.dgSetColor(player.name, player.color);
                    if (oldPlayers[index].info.IsDead != player.info.IsDead && player.info.IsDead == 1) // morreu
                        conn.ghost(autoAmongForm.dgDiscordIdByName(player.name));
                    if (oldPlayers[index].info.Disconnected != player.info.Disconnected && player.info.Disconnected == 1) // desconectou
                        autoAmongForm.dgRemove(player.name);

                }
            foreach (Player player in oldPlayers)
                if (players.FindIndex(r => r.name.Equals(player.name)) == -1) // Saiu
                    autoAmongForm.dgRemove(player.name);

            oldPlayers = players;
        }
    }
}
