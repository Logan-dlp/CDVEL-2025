using UnityEngine;
using System;

namespace Timers
{
    using Singletons;
    
    public class TimerHandler : MonoSingleton<TimerHandler>
    {
        public event Action OnStartedTimer;
        public event Action OnStoppedTimer;
        public event Action OnResetTimer;
        public event Action OnTimerFinished;
        public event Action OnFinalCountdownLauch;
        
        public event Action<float> OnTimerUpdate; 
        
        [SerializeField] private float _timer;
        
        private ITimerSystem _timerSystem;

        protected override void Awake()
        {
            base.Awake();
            
            _timerSystem = new TimerSystem(_timer);
            
            OnStartedTimer += _timerSystem.StartTimer;
            OnStoppedTimer += _timerSystem.StopTimer;
            OnResetTimer += _timerSystem.ResetTimer;

            _timerSystem.OnTimerFinished += () => OnTimerFinished?.Invoke();
            _timerSystem.OnFinalCountdownLauch += () => OnFinalCountdownLauch?.Invoke();
            _timerSystem.OnTimerUpdate += (timer) => OnTimerUpdate?.Invoke(timer);
        }

        private void Start()
        {
            OnTimerUpdate?.Invoke(_timer);
        }

        private void Update()
        {
            _timerSystem.UpdateTimer();
        }
        
        private void OnDestroy()
        {
            OnStartedTimer -= _timerSystem.StartTimer;
            OnStoppedTimer -= _timerSystem.StopTimer;
            OnResetTimer -= _timerSystem.ResetTimer;
        }

        [ContextMenu("Start Timer")]
        public void StartTimer()
        {
            OnStartedTimer?.Invoke();
        }

        [ContextMenu("Stop Timer")]
        public void StopTimer()
        {
            OnStoppedTimer?.Invoke();
        }

        [ContextMenu("Reset Timer")]
        public void ResetTimer()
        {
            OnResetTimer?.Invoke();
        }
    }
}