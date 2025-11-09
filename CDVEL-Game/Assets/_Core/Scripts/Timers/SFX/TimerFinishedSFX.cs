using Audio;
using Timers;
using UnityEngine;

public class TimerFinishedSFX : SFXLauncher
{
    [SerializeField] private SoundData _finalCountdownSound;

    protected override void Awake()
    {
        base.Awake();
        if (_fx == null) _fx = _finalCountdownSound;
        TimerHandler.Instance.OnFinalCountdownLauch += PlayFX;
        TimerHandler.Instance.OnTimerFinished += StopSFX;
    }

    private void OnDestroy()
    {
        TimerHandler.Instance.OnFinalCountdownLauch -= PlayFX;        
        TimerHandler.Instance.OnTimerFinished -= StopSFX;
    }
}