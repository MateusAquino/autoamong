using System.Net;
using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Windows.Forms;
using AutoAmongUs;

namespace AutoAmong
{
    public class Connection
    {
        private static string version = "4";
        private const string ip = "localhost"; // ip do host
        private const int port = 3000; // porta do host
        private string hash;

        public Connection(string hash) {
            this.hash = hash;
        }

        public static bool ping(string hash) {
            string res = new Connection(hash).Send("GET", "ping", $"hash={hash}&v={version}");
            if (res == "\"update\"") {
                Form1.error.Text = "Versão desatualizada!\nBaixe a mais recente no canal de mods.";
                Form1.error.Visible = true;
            }
            return res=="true";
        }

        public void setUserIGN(string id, string nick) {
            Send("PUT", "nickname", $"id={id}&nick={nick}&hash={hash}");
        }

        public void forceMute(string id, bool mute) {
            if (id != "<Unlinked>")
                Send("PUT", "ghost", $"id={id}&mute={(mute ? "true" : "false")}&hash={hash}");
        }

        public void ghost(string id) {
            if (id!="<Unlinked>")
                Send("PUT", "ghost", $"id={id}&mute={("true")}&hash={hash}");
        }

        public void startGame() {
            Send("POST", "startgame", $"hash={hash}");
        }

        public void inGame(bool inGame) {
            Send("POST", "ingame", $"ingame={(inGame?"true":"false")}&hash={hash}");
        }

        public void endGame() {
            Send("POST", "endgame", $"hash={hash}");
        }

        public List<Dictionary<string,string>> discordUsers = new List<Dictionary<string, string>>();
        public void refreshDiscord() {
            ClientState state = AmongMemory.clientState;
            string[] codeRegion = state == ClientState.InLobby ? AmongUsData.GetCodeAndRegion() : new string[] { "??????", "??" };
            string users = Send("GET", "usersInVoiceChannel", $"hash={hash}&state={state}&code={codeRegion[0]}&region={codeRegion[1]}").Trim();

            if (users == "") {
                Console.WriteLine("Nenhum usuário no canal de voz sincronizado, voltando...");
                Form1.error.Text = "Nenhum usuário no canal de voz sincronizado.";
                Form1.error.Visible = true;
                Form2.exitOnClose = false;
                AmongMemory.Stop();
                try { if (Form2.amongThread!=null) Form2.amongThread.Interrupt(); } catch (Exception) { }
                Form2.form.Close();
            }

            using (StringReader reader = new StringReader(users)) {
                discordUsers = new List<Dictionary<string, string>>();
                string line = string.Empty;
                int userIndex = -1;
                do {
                    line = reader.ReadLine();
                    if (line != null) {
                        var spacing = Regex.Matches(line, @"^ +").Count;
                        if (spacing == 0) {
                            userIndex++;
                            discordUsers.Add(new Dictionary<string, string>());
                        } else {
                            string[] keyValue = line.Split(':');
                            discordUsers[userIndex][keyValue[0].Trim()] = keyValue[1].Trim();
                        }
                    }
                } while (line != null);
            }
        }

        public string Send(string method, string endpoint, string data) {
            string url = $"http://{ip}:{port}/api/{endpoint}";
            if (method == "GET")
                url += "?" + data;
            HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(url);
            request.Method = method;
            request.AllowAutoRedirect = false;
            request.Accept = "text/html,text/plain,application/json,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.ContentType = "application/x-www-form-urlencoded";
            request.MaximumAutomaticRedirections = 4;

            if (method != "GET") {
                using (StreamWriter stOut = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.UTF8)) {
                    stOut.Write(data);
                    stOut.Close();
                }
            }

            HttpWebResponse response;
            try {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception) {
                return "";
            }

            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, System.Text.Encoding.UTF8);

            //Console.WriteLine("Response stream received:");
            string res = readStream.ReadToEnd();
            //Console.WriteLine(res);
            response.Close();
            readStream.Close();
            return res;
        }
    }
}
