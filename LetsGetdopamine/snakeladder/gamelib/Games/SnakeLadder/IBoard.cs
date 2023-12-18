namespace gamelib.V2
{
    public interface IBoard
    {
        public void Initialize(IRuleProvider ruleProvider);
        public void PlayTurn();
        public bool IsGameOver { get; }
        public void PrintWinner();
    }
}
