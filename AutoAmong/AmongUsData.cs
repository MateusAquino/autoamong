using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AutoAmongUs
{
    public class AmongUsData
    {
        public static IntPtr gameAssembly = IntPtr.Zero;

        public static bool Init()
        {
            bool procFound = Memory.OpenProcess("Among Us");

            if (!procFound) return false;

            Memory.Module gameAssemblyModule = Memory.GetModuleByName("GameAssembly.dll");

            if (gameAssemblyModule == null) return false;

            gameAssembly = gameAssemblyModule.BaseAddress;

            return procFound;
        }

        // 0 = Main Menu, 1 = In lobby, 2 = Playing, 3 = End game Menu
        public static ClientState GetClientState()
        {
            IntPtr clientStatePtr = Memory.GetRealAddress(gameAssembly + Offsets.gameStateAddress, Offsets.gameStateOffsets);

            int clientStateValue = Memory.ReadInt(clientStatePtr + 0x64, 0);

            ClientState clientState = (ClientState)Enum.Parse(typeof(ClientState), clientStateValue.ToString(), true);

            return clientState;
        }

        // 0 = Discussion, 1 = Not Voted, 2 = Voted, 3 = Voting ended scene, 4 = Playing
        public static MatchState GetMatchState()
        {
            IntPtr matchStatePtr = Memory.GetRealAddress(gameAssembly + Offsets.discussionStateAddress, Offsets.discussionStateOffsets);

            int matchStateValue = Memory.ReadInt(matchStatePtr + 0x84, 4);

            MatchState matchState = (MatchState)Enum.Parse(typeof(MatchState), matchStateValue.ToString(), true);

            return matchState;
        }

        public static string[] GetCodeAndRegion()
        {
            IntPtr gameStatePtr = Memory.GetRealAddress(gameAssembly + Offsets.gameStartManagerAddress, Offsets.gameStartManagerOffsets);
            string gameCode = Memory.ReadString(gameStatePtr);
            string[] split;
            if (gameCode != null && gameCode.Length > 0 && (split = gameCode.Split('\n')).Length == 2)
            {
                IntPtr regionPtr = Memory.GetRealAddress(gameAssembly + Offsets.serverManagerAddress, Offsets.serverManagerOffsets);
                PlayRegion region = (PlayRegion)((4 - (Memory.ReadInt(regionPtr + 0x8, 0) & 0b11)) % 3); // do NOT ask
                return new string[]{ gameCode, region.ToString() };
            }
            return new string[]{ "??????", "??" };
        }

        public static List<Player> GetPlayers()
        {
            IntPtr playerList = Memory.GetRealAddress(gameAssembly + Offsets.playerListAddress, Offsets.playerListOffsets);
            IntPtr playerEntityList = Memory.GetRealAddress(playerList + 0x8, null);

            List<Player> players = new List<Player>();

            int totalPlayers = Memory.ReadInt(playerList + 0x0c, 0);

            int playerOffset = 0x4;
            int currentPlayer = 0x10;

            for (int i = 0; i < totalPlayers; i++)
            {
                IntPtr playerPtr = Memory.GetRealAddress(playerEntityList + currentPlayer, null);

                PlayerInfo playerInfo = Memory.Read<PlayerInfo>(Memory.ReadBytes(playerPtr, Marshal.SizeOf(typeof(PlayerInfo))));

                IntPtr playerNamePtr = Memory.GetRealAddress(playerPtr + 0xc, null);

                string playerName = Memory.ReadString(playerNamePtr);

                Player player = new Player(playerName, playerInfo);

                players.Add(player);

                currentPlayer += playerOffset;
            }

            return players;
        }
    }
}
