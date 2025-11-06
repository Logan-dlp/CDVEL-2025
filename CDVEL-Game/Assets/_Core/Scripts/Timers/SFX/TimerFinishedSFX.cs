using Audio;
using Timers;
using UnityEngine;

public class TimerFinishedSFX : SFXLauncher
{
    [SerializeField] private SoundData _finalCountdownSound;
    [SerializeField] private SoundData _victorySound;

    protected override void Awake()
    {
        base.Awake();
        if (_fx == null) _fx = _finalCountdownSound;
        TimerHandler.Instance.OnFinalCountdownLauch += PlayFX;
        _fx = _victorySound;
        TimerHandler.Instance.OnTimerFinished += StopSFX;
        TimerHandler.Instance.OnTimerFinished += PlayFX;
    }

    private void OnDestroy()
    {
        TimerHandler.Instance.OnFinalCountdownLauch -= PlayFX;        
        TimerHandler.Instance.OnTimerFinished -= StopSFX;
        TimerHandler.Instance.OnTimerFinished -= PlayFX;
    }
}