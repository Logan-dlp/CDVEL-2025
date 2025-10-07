using TMPro;
using UnityEngine;

namespace Timers
{
    using UI;
    
    public class DisplayTimer : MonoBehaviour, IDisplay
    {
        [SerializeField] private TextMeshProUGUI _timerText;

        private float _currentTimer;

        private void Awake()
        {
            TimerHandler.Instance.OnTimerUpdate += ChangeTimer;
        }

        private void ChangeTimer(float timer)
        {
            _currentTimer = timer;
            Refresh();
        }
        
        public void Refresh()
        {
            float minutes = Mathf.FloorToInt(_currentTimer / 60);
            float seconds = Mathf.FloorToInt(_currentTimer % 60);
            float milliseconds = _currentTimer % 1 * 1000;
            
            _timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        }
    }
}