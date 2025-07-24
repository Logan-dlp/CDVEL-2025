using UnityEngine;
using System;

namespace Scores
{
    public class ScoreHandler : MonoBehaviour
    {
        public event Action<int> OnScoreUpdated;
        
        private IScoreSystem _scoreSystem;

        private void Awake()
        {
            _scoreSystem = new ScoreSystem();
        }

        private void Start()
        {
            OnScoreUpdated?.Invoke(_scoreSystem.Score);
        }

        public void OnAddedPoints(int points)
        {
            _scoreSystem.AddPoints(points);
        }
    }
}