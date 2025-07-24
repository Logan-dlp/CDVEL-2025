using UnityEngine;
using TMPro;

namespace Scores
{
    public class ScoreViewer : MonoBehaviour
    {
        [SerializeField] private ScoreHandler _scoreSystem;
        [SerializeField] private TextMeshProUGUI _scoreText;

        private void Awake()
        {
            if (_scoreSystem != null)
            {
                _scoreSystem.OnScoreUpdated += UpdateScoreView;
            }
        }

        private void OnDestroy()
        {
            if (_scoreSystem != null)
            {
                _scoreSystem.OnScoreUpdated -= UpdateScoreView;
            }
        }

        private void UpdateScoreView(int score)
        {
            if (_scoreText == null)
                return;
            
            _scoreText.text = score.ToString();
        }
    }
}