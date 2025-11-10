using UnityEngine.Events;
using UnityEngine;

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
