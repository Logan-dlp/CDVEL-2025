using UnityEngine;

namespace Tram
{
    using Timers;
    
    public class ActiveTramBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject[] _tramGameObject;
        
        private int _tramIterator;
        
        private float _timer;
        private float _passageTime;

        private void Awake()
        {
            TimerHandler.Instance.OnStartedTimer += SetTimer;
            TimerHandler.Instance.OnTimerUpdate += UpdatePassage;
        }

        private void OnDestroy()
        {
            TimerHandler.Instance.OnStartedTimer -= SetTimer;
            TimerHandler.Instance.OnTimerUpdate -= UpdatePassage;
        }

        private void SetTimer()
        {
            _timer = TimerHandler.Instance.MaxTimer;
            _passageTime = _timer / (_tramGameObject.Length + 1);
            
        }

        private void UpdatePassage(float time)
        {
            if (time < _timer - _passageTime * (_tramIterator + 1) && _tramIterator < _tramGameObject.Length)
            {
                _tramGameObject[_tramIterator].SetActive(true);
                _tramIterator++;
            }
        }
    }
}