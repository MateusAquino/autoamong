namespace AutoAmongUs
{
    class Offsets
    {
        // https://github.com/denverquane/amonguscapture/blob/master/Offsets.json
        // Meeting HUD
        public static int discussionStateAddress = 0x1C573A4;
        public static int[] discussionStateOffsets = { 0x5c, 0 };

        // Client
        public static int gameStateAddress = 0x1C57F54;
        public static int[] gameStateOffsets = { 0x5c, 0 };

        public static int gameStartManagerAddress = 0x1AF20FC;
        public static int[] gameStartManagerOffsets = { 0x5c, 0, 0x20, 0x28 };

        public static int serverManagerAddress = 0x1AE4DEC;
        public static int[] serverManagerOffsets = { 0x5c, 0, 0x10, 0x8 };

        // GameData
        public static int playerListAddress = 0x1C57BE8;
        public static int[] playerListOffsets = { 0x5c, 0, 0x24 };
    }
}
