namespace AutoAmongUs
{
    public class Player
    {
        public string name;
        public byte color;
        public PlayerInfo info;

        public Player(string name, PlayerInfo info)
        {
            this.name = name;
            this.info = info;

            this.color = info.ColorId;
        }
    }
}
