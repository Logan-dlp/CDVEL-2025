using UnityEngine;
using Timers;
using Audio;
using FX;

public class TimerStartedCounterSFX : SFXLauncher
{
    [SerializeField] private SoundData _beginGameSound;

    protected override void Awake()
    {
        base.Awake();
        if (_fx == null) _fx = _beginGameSound;
        TimerHandler.Instance.OnStartedTimer += PlayFX;
    }

    private void OnDestroy()
    {
        TimerHandler.Instance.OnStartedTimer -= PlayFX;
    }
}