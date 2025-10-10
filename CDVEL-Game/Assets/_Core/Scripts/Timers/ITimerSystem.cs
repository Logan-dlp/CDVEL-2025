using System;

namespace Timers
{
    public interface ITimerSystem
    {
        public event Action OnTimerFinished;
        public event Action<float> OnTimerUpdate;
        
        public float CurrentTimer { get; }
        public float Timer { get; }
        
        public void StartTimer();
        public void UpdateTimer();
        public void StopTimer();
        public void ResetTimer();
    }
}