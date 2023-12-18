namespace SnakeLadder;
public class Block : IBlock{
    private readonly int next;
    private readonly int current;
    private readonly string blockType;
    public Block(int next,int current, string blockType){
        this.next = next;
        this.current = current;
        this.blockType = blockType;
    }

    public void Print(){
        if(this.blockType=="snake"){
            var color = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"Sorry?? You are bitten by Snake, reducing from {current} to {next}");
            Console.BackgroundColor = color;
        }
        else if (this.blockType == "ladder"){
            var color = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine($"Congratulation! You found a Ladder, Advancing from {current} to {next}");
            Console.BackgroundColor = color;
        }
    }
    public int Next(){
        return this.next;
    }
}