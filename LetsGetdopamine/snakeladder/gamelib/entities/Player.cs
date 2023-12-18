namespace SnakeLadder;
public class  Player : IPlayer{
    private readonly string name;
    private int currentPosition;
    public Player(string name){
        this.name=name;
        this.currentPosition = 0;
    }

    public void Print(){
        Console.WriteLine($"{this.name} your are at {this.currentPosition}");
    }
    public void RequestForDiceRoll(){
        Console.WriteLine($"{this.name}({this.currentPosition}) your turn, please hit enter to roll the dice");
        var key = Console.ReadKey();
    }
    public int RollDice(){
        Random rnd = new Random();
        int dice   = rnd.Next(1, 7);   // creates a number between 1 and 6
        var color = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("___");
        Console.WriteLine($"|{dice}|");
        Console.WriteLine("***");
        Console.ForegroundColor = color;
        return dice;
    }
    public int GetCurrentPosition(){
        return this.currentPosition;
    }
    public void SetNextPosition(int nextPosition){
        this.currentPosition=nextPosition;
    }
    public string GetName(){
        return this.name;
    }


}