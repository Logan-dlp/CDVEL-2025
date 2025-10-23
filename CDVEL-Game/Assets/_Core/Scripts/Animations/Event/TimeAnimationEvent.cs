using UnityEngine;
using UnityEngine.Events;

namespace Animations.Event
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