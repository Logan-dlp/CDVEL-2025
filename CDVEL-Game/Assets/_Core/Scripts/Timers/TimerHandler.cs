using System;
using UnityEngine;

namespace Timers
{
    using Singletons;
    
    public class TimerHandler : MonoSingleton<TimerHandler>
    {
        public event Action OnStartedTimer;
        public event Action OnStoppedTimer;
        public event Action OnResetTimer;
        
        public event Action<float> OnTimerUpdate; 
        
        [SerializeField] private float _timer;
        
        private ITimerSystem _timerSystem;

        private void Update()
        {
            _timerSystem?.UpdateTimer();
        }

        protected override void Awake()
        {
            base.Awake();
            _timerSystem = new TimerSystem(_timer);
            
            OnStartedTimer += _timerSystem.StartTimer;
            OnStoppedTimer += _timerSystem.StopTimer;
            OnResetTimer += _timerSystem.ResetTimer;
            
            _timerSystem.OnTimerUpdate += OnTimerUpdate;
            
            OnTimerUpdate?.Invoke(_timer);
        }

        public void OnDestroy()
        {
            OnStartedTimer -= _timerSystem.StartTimer;
            OnStoppedTimer -= _timerSystem.StopTimer;
            OnResetTimer -= _timerSystem.ResetTimer;
            
            _timerSystem.OnTimerUpdate -= OnTimerUpdate;
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