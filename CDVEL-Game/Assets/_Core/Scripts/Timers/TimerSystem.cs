using System;
using UnityEngine;

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
            ResetTimer();
        }
        
        public void StartTimer()
        {
            _isRunning = true;
        }

        public void UpdateTimer()
        {
            if (_isRunning)
            {
                _timer -= Time.deltaTime;
                OnTimerUpdate?.Invoke(_timer);
                if (_timer <= 0)
                {
                    StopTimer();
                    OnTimerFinished?.Invoke();
                    StopTimer();
                }
            }
        }

        public void StopTimer()
        {
            _isRunning = false;
        }

        public void ResetTimer()
        {
            _currentTimer = _timer;
        }
    }
}