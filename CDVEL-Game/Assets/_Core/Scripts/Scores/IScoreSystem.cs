namespace Scores
{
    public interface IScoreSystem
    {
        public int Score { get; }
        public void AddPoints(int points);
    }
}