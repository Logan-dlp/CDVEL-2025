using UnityEngine;
using UnityEngine.Events;

namespace Animations
{
    using Timers;
    
    public class TimeAnimationEvent : MonoBehaviour, IAnimationEvent
    {
        [SerializeField] private UnityEvent _callback;
        
        public void OnAnimationEvent()
        {
            TimerHandler.Instance.StartTimer();
            _callback?.Invoke();
        }
    }
}