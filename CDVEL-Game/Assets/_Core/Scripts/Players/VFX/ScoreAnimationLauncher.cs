using UnityEngine;
using TMPro;

namespace Players.Movements
{
    using Scores;
    using FX;
    
    public class ScoreAnimationLauncher : FXLauncher<Animator>
    {
        [SerializeField] private ScoreHandler _scoreHandler;
        
        private TextMeshProUGUI _text;

        protected override void Awake()
        {
            base.Awake();
            _text = GetComponent<TextMeshProUGUI>();
            _scoreHandler.OnAddedPoint += SetPoint;
        }

        protected override void PlayFX()
        {
            _fx.SetTrigger("IsPopIn");
        }

        private void SetPoint(Vector3 position, int point)
        {
            transform.position = Camera.main.WorldToScreenPoint(position);
            string positive = (point > 0) ? "+" : "";
            _text.text = $"{positive}{point}";
            PlayFX();
        }
    }
}