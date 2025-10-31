using UnityEngine;

namespace Players
{
    using Scores;
    
    public class PlayerScoreHandler : ScoreHandler
    {
        [SerializeField] private PlayerTag _playerTag;
        public PlayerTag PlayerTag => _playerTag;
    }
}