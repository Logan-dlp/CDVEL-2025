namespace Scores
{
    public class ScoreSystem : IScoreSystem
    {
        private int _score;
        public int Score => _score;
        
        public void AddPoints(int points)
        {
            _score += points;
        }
    }
}