namespace Scores
{
    public class ScoreSystem : IScoreSystem
    {
        private int _score;
        public int Score => _score;
        
        public void AddPoints(int points)
        {
            if (_score + points < 0)
            {
                _score = 0;
                return;
            }
            
            _score += points;
        }
    }
}