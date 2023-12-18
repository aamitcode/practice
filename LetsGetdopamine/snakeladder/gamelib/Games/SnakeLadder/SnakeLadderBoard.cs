namespace gamelib.V2
{
    public class SnakeLadderBoard : IBoard
    {
        private readonly Queue<PlayerBlockMap> playersQueue = new Queue<PlayerBlockMap>();
        private readonly IBlock[] blocks;
        private readonly IDice dice;
        private bool gameOver;
        private IPlayer? winner;

        /// <summary>
        /// Create a board
        /// </summary>
        /// <param name="players">List of player</param>
        /// <param name="dice">Dice</param>
        public SnakeLadderBoard(List<IPlayer> players, IDice dice)
        {
            this.dice = dice;
            this.blocks = new Block[100];
            this.winner = null;
            for (int i = 0; i < 100; i++)
            {
                this.blocks[i] = new Block(BlockType.None, i);
            }
            foreach (var player in players)
            {
                var playerPosition = new PlayerBlockMap() { Player = player, Block = this.blocks[0] };
                this.playersQueue.Enqueue(playerPosition);
            }
            gameOver = false;
        }

       /// <summary>
       /// Initialize the Board with rules
       /// </summary>
       /// <param name="rules">Rule Set</param>
        public void Initialize(IRuleProvider ruleProvider)
        {
            int[,] rules = ruleProvider.GetRules();
            for (int i = 0; i < rules.GetLength(0); i++)
            {
                this.blocks[rules[i, 0]] = new Block(rules[i, 0] > rules[i, 1] ? BlockType.Snake : BlockType.Ladder, rules[i, 1]);
            }
        }

        /// <summary>
        /// Play Turn 
        ///     - Get the player from queue
        ///     - Roll the dice
        ///     - Check game over
        ///     - update the board
        ///     - requeu the player
        /// </summary>
        public void PlayTurn()
        {
            var currentPlayer = playersQueue.Dequeue();
            Console.WriteLine($"{currentPlayer.Player.GetPlayerIdentifier()} [@:{currentPlayer.Block.Destination}] ==> roll to dice (HIT ENTER)");
            Console.ReadKey();
            var diceNumber = this.dice.Roll();
            var playerNewPosition = currentPlayer.Block.Destination + diceNumber;
            if (!this.CheckGameOver(playerNewPosition))
            {
                var nextBlock = this.blocks[playerNewPosition];
                Print(nextBlock, currentPlayer);
                currentPlayer.Block = nextBlock;
                playersQueue.Enqueue(currentPlayer);
            }
            else
            {
                Console.WriteLine($"{currentPlayer.Player.GetPlayerIdentifier()}, You WIN!!!!");
                this.winner = currentPlayer.Player;
            }
        }

        /// <summary>
        /// Game Over property
        /// </summary>
        public bool IsGameOver
        {
            get
            {
                return this.gameOver;
            }
        }

        /// <summary>
        /// Print the Winner
        /// </summary>
        public void PrintWinner()
        {
            Console.WriteLine($"Ladies and Gentlement and WINNER IS :{this.winner.GetPlayerIdentifier()}");
        }

        /// <summary>
        /// Check if Game over
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool CheckGameOver(int pos)
        {
            this.gameOver = pos > 99;
            return this.gameOver;
        }

        /// <summary>
        /// Print Post dice roll
        /// </summary>
        /// <param name="nextBlock">next block</param>
        /// <param name="currentPlayer">current player</param>
        private static void Print(IBlock nextBlock, PlayerBlockMap currentPlayer)
        {
            string playerIdentifer = currentPlayer.Player.GetPlayerIdentifier();
            var playerBlockPosition = currentPlayer.Block.Destination;
            switch (nextBlock.BlockType)
            {
                case BlockType.Snake:
                    Console.WriteLine($"{playerIdentifer}, Hard Luck found a SNAKE,==> reducing from :{playerBlockPosition} to :{nextBlock.Destination}");
                    break;
                case BlockType.Ladder:
                    Console.WriteLine($"{playerIdentifer}, Amazing found a LADDER ==> Advancing from :{playerBlockPosition} to :{nextBlock.Destination}");
                    break;
                default:
                    Console.WriteLine($"{playerIdentifer} ==> Advancing from :{playerBlockPosition} to :{nextBlock.Destination}");
                    break;
            }
        }
    }
}
