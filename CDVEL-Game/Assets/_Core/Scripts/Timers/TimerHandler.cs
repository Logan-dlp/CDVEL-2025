using System;
using UnityEngine;

namespace Timers
{
    using SceneLoader;
    using Singletons;

    public class TimerHandler : MonoSingleton<TimerHandler>
    {
        [SerializeField] private string _sceneName;
        [SerializeField] private SaveTotalScore saveTotalScore;
        public event Action OnStartedTimer;
        public event Action OnStoppedTimer;
        public event Action OnResetTimer;
        public event Action OnTimerFinished;
        public event Action OnFinalCountdownLauch;

        public event Action<float> OnTimerUpdate;


        [SerializeField] private float _timer;
        public float MaxTimer => _timer;

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

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                SceneLoad();
            }
        }

        private void OnDestroy()
        {
            if (_timerSystem == null)
                return;

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

        [ContextMenu("Scene Score Load")]
        public void SceneLoad()
        {
            saveTotalScore.SaveScoreTotal();
            SceneLoaderHandler.Instance.LoadScene(_sceneName);
        }
    }
}