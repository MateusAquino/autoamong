namespace AutoAmongUs
{
    class Offsets
    {
        // https://github.com/denverquane/amonguscapture/blob/111b7f594887ff551ccc89008f1940f1bc9b9cb7/AmongUsCapture/Settings.cs
        // Meeting HUD
        public static int discussionStateAddress = 0x144B7CC;
        public static int[] discussionStateOffsets = { 0x5c, 0 };

        // Client
        public static int gameStateAddress = 0x144BB30;
        public static int[] gameStateOffsets = { 0x5c, 0 };

        public static int gameStartManagerAddress = 0x13A715C;
        public static int[] gameStartManagerOffsets = { 0x5c, 0, 0x20, 0x28 };

        public static int serverManagerAddress = 0x139CF7c;
        public static int[] serverManagerOffsets = { 0x5c, 0, 0x10, 0x8 };

        // GameData
        public static int playerListAddress = 0x144BA30;
        public static int[] playerListOffsets = { 0x5c, 0, 0x24 };
    }
}
