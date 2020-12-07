namespace AutoAmongUs
{
    class Offsets
    {
        // https://github.com/denverquane/amonguscapture/blob/master/Offsets.json
        // Meeting HUD
        public static int discussionStateAddress = 0x1BE0CB4;
        public static int[] discussionStateOffsets = { 0x5c, 0 };

        // Client
        public static int gameStateAddress = 0x1BE1074;
        public static int[] gameStateOffsets = { 0x5c, 0 };

        public static int gameStartManagerAddress = 0x1B5AB00;
        public static int[] gameStartManagerOffsets = { 0x5c, 0, 0x20, 0x28 };

        public static int serverManagerAddress = 0x1B4D7F0;
        public static int[] serverManagerOffsets = { 0x5c, 0, 0x10, 0x8 };

        // GameData
        public static int playerListAddress = 0x1BE0BB8;
        public static int[] playerListOffsets = { 0x5c, 0, 0x24 };
    }
}
