namespace SnakeLadder;
public class Board : IBoard{
    private IBlock[] blocks;
    private readonly Queue<IPlayer> players;
    private bool gameOver;
    public Board(){
        this.gameOver=false;
        this.blocks = new Block[100];
        for(int i=0;i<100;i++){
            this.blocks[i]= new Block(i,i,"baseBlock");
        }
        players = new Queue<IPlayer>();
    }
    public void Initialize(){
        Console.WriteLine("Initializing board");
        var snakes = File.ReadAllLines(@"C:\git\practice\LetsGetdopamine\snakeladder\gamelib\rule\snakeLadder\snakes.csv");
        foreach(var snake in snakes){
            var coordinate = snake.Split(',');
            int startPosition = Convert.ToInt32(coordinate[0])-1;
            int endPosition = Convert.ToInt32(coordinate[1])-1;
            this.blocks[startPosition] =  new Block(endPosition,startPosition,"snake");//(IBlock) new SnakeBlock(endPosition);
        }
        var ladders = File.ReadAllLines(@"C:\git\practice\LetsGetdopamine\snakeladder\gamelib\rule\snakeLadder\ladder.csv");
        foreach(var ladder in ladders){
            var coordinate = ladder.Split(',');
            int startPosition = Convert.ToInt32(coordinate[0])-1;
            int endPosition = Convert.ToInt32(coordinate[1])-1;
            blocks[startPosition] = new Block(endPosition,startPosition,"ladder");
        }
        Console.WriteLine("Initialized board");
    }

    public void Join(IPlayer player){
        Console.WriteLine($"{player.GetName()} joining the game");
        this.players.Enqueue(player);
    }
    public bool isGameOver(){
        return this.gameOver;
    }
    public int GetNextPosition(int newPos){
        if(newPos >=100){
            this.gameOver=true;
            return 101;
        }
        var blk = this.blocks[newPos];
        blk.Print();
        return blk.Next();
    }

    public void SetNextPlayer(){
        var nextPlayer = this.players.Dequeue();
        this.players.Enqueue(nextPlayer);
    }
    public IPlayer CurrentPlayer(){
            return this.players.Peek();
    }
    public void Update(int diceNumber){
        var nextPosition = this.GetNextPosition(this.CurrentPlayer().GetCurrentPosition() +diceNumber);
        this.CurrentPlayer().SetNextPosition(nextPosition);
        this.CurrentPlayer().Print();
    }
}