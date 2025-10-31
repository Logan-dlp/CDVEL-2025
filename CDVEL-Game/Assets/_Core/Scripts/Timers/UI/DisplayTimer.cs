using UnityEngine;
using TMPro;

namespace Timers
{
    using UI;
    
    public class DisplayTimer : MonoBehaviour, IDisplay
    {
        private TextMeshProUGUI _timerText;
        private float _currentTimer;

        private void Awake()
        {
            _timerText = GetComponent<TextMeshProUGUI>();
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
            
            _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}