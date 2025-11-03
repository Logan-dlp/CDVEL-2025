using UnityEngine;
using Timers;
using Audio;
using FX;

public class TimerStartedAmbiantMusicSFX : SFXLauncher
{
    [SerializeField] private SoundData _ambiantMusicSound;

    protected override void Awake()
    {
        base.Awake();
        if (_fx == null) _fx = _ambiantMusicSound;
        TimerHandler.Instance.OnStartedTimer += PlayFX;
    }

    private void OnDestroy()
    {
        TimerHandler.Instance.OnStartedTimer -= PlayFX;
    }
}