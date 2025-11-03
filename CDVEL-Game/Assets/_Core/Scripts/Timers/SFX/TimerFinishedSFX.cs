using Audio;
using Timers;
using UnityEngine;

public class TimerFinishedSFX : SFXLauncher
{
    [SerializeField] private SoundData _finishSound;

    protected override void Awake()
    {
        base.Awake();
        if (_fx == null) _fx = _finishSound;
        TimerHandler.Instance.OnTimerFinished += PlayFX;
        TimerHandler.Instance.OnTimerFinished += StopSFX;
    }

    private void OnDestroy()
    {
        TimerHandler.Instance.OnTimerFinished -= PlayFX;
        TimerHandler.Instance.OnTimerFinished -= StopSFX;
    }
}