namespace SnakeLadder;
public interface IBoard{
    public void Initialize();
    public void Join(IPlayer player);

    public void SetNextPlayer();

    public bool isGameOver();
    public IPlayer CurrentPlayer();
    public void Update(int diceNumber);
}