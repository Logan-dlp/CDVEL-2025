using UnityEngine;

namespace Timers.Events
{
    public class OnTimerFinished : MonoBehaviour
    {
        [SerializeField] private GameObject _timerObject;

        private void Start()
        {
            _timerObject.SetActive(false);
            TimerHandler.Instance.OnTimerFinished += OnTimerFinish;
        }

        private void OnTimerFinish()
        {
            _timerObject.SetActive(true);
        }
    }
}