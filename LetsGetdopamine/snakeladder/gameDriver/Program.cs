// See https://aka.ms/new-console-template for more information
using SnakeLadder;
IPlayer redPlayer = new Player("Red");
IPlayer greenPlayer = new Player("Green");
IBoard board = new Board();
board.Initialize();
board.Join(redPlayer);
board.Join(greenPlayer);
while(!board.isGameOver()){
    Console.WriteLine("---------------------------------------------------");
    board.CurrentPlayer().RequestForDiceRoll();
    board.Update(board.CurrentPlayer().RollDice());
    Console.WriteLine("---------------------------------------------------");
    if(!board.isGameOver()){
        board.SetNextPlayer();
    }
    else{
        Console.WriteLine($"game over and winner is {board.CurrentPlayer().GetName()}")
    }
}