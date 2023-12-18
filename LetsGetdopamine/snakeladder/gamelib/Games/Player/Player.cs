namespace gamelib.V2
{
    public class Player : IPlayer
    {
        private readonly string playerIdentifier;

        public Player(string playerIdentifier)
        {
            this.playerIdentifier = playerIdentifier;
        }

        public string GetPlayerIdentifier()
        {
            return this.playerIdentifier;
        }
    }
}
