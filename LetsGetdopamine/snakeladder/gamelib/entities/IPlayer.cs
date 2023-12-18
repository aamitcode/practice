namespace SnakeLadder;
public interface IPlayer{

    public void Print();
    public void RequestForDiceRoll();
    public int RollDice();

    public void SetNextPosition(int nextPosition);
    public int GetCurrentPosition();
    public string GetName();

}