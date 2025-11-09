using Audio;
using Timers;
using UnityEngine;

public class TimerVictorySFX : SFXLauncher
{
    [SerializeField] private SoundData _victorySound;

    protected override void Awake()
    {
        base.Awake();
        if (_fx == null) _fx = _victorySound;
        TimerHandler.Instance.OnTimerFinished += PlayFX;        
    }

    private void OnDestroy()
    {
        TimerHandler.Instance.OnTimerFinished -= PlayFX;
    }
}