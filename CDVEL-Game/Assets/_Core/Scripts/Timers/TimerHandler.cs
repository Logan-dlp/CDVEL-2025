using System;
using UnityEngine;

namespace Timers
{
    using Singletons;
    
    public class TimerHandler : MonoSingleton<TimerHandler>
    {
        public event Action OnStartedTimer;
        public event Action OnStoppedTimer;
        
        public event Action<float> OnTimerUpdate; 
        
        [SerializeField] private float _timer;
        
        private ITimerSystem _timerSystem;

        private void Update()
        {
            _timerSystem.UpdateTimer();
        }

        protected override void OnInitializing()
        {
            base.OnInitializing();
            _timerSystem = new TimerSystem(_timer);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            OnStartedTimer += _timerSystem.StartTimer;
            _timerSystem.OnTimerUpdate += OnTimerUpdate;
            OnStoppedTimer += _timerSystem.StopTimer;
        }

        public override void Uninitialize()
        {
            base.Uninitialize();
            OnStartedTimer -= _timerSystem.StartTimer;
            _timerSystem.OnTimerUpdate -= OnTimerUpdate;
            OnStoppedTimer -= _timerSystem.StopTimer;
        }
    }
}