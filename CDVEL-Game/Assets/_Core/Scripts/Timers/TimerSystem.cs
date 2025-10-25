using UnityEngine;
using System;

namespace Timers
{
    public class TimerSystem : ITimerSystem
    {
        public event Action OnTimerFinished;
        public event Action<float> OnTimerUpdate;
        
        private float _currentTimer;
        public float CurrentTimer => _currentTimer;

        private float _timer;
        public float Timer => _timer;
        
        private bool _isRunning;

        public TimerSystem(float timer)
        {
            _timer = timer;
            _currentTimer = _timer;
            Time.timeScale = 0;
        }
        
        public void StartTimer()
        {
            _isRunning = true;
            Time.timeScale = 1;
        }

        public void UpdateTimer()
        {
            if (_isRunning)
            {
                _currentTimer -= Time.deltaTime;
                if (_currentTimer <= 0)
                {
                    _currentTimer = 0;
                    StopTimer();
                    OnTimerFinished?.Invoke();
                }
                OnTimerUpdate?.Invoke(_currentTimer);
            }
        }

        public void StopTimer()
        {
            _isRunning = false;
            Time.timeScale = 0;
        }

        public void ResetTimer()
        {
            _currentTimer = _timer;
            OnTimerUpdate?.Invoke(_currentTimer);
        }
    }
}