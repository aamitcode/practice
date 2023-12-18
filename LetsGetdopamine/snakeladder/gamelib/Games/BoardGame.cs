namespace gamelib.V2
{
    public class BoardGame
    {
        public static void Play(GameType gameType)
        {
            IBoard board = GetGame(gameType);
            board.Initialize(new RuleProvider());

            while (!board.IsGameOver)
            {
                board.PlayTurn();
            }

            board.PrintWinner();

            Console.WriteLine("Hit Enter to Close the Window!!!");
            Console.ReadKey();
        }

        public static List<IPlayer> AddPlayer()
        {
            List<IPlayer> players = new List<IPlayer>();
            players.Add(new Player("RED"));
            players.Add(new Player("BLUE"));
            players.Add(new Player("GREEN"));
            return players;
        }
        public static IBoard GetGame(GameType gameType)
        {
            IBoard board = null;
            switch (gameType)
            {
                case GameType.SnakeLadder:
                    board = new SnakeLadderBoard(AddPlayer(), new Dice());
                    break;
                default:
                    throw new ArgumentException("Wrong Game type");
            }
            return board;
        }
    }
}
