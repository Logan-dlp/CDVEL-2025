using UnityEngine;
using Audio;
using Hearts;

public class BumperHeartSFX : SFXLauncher
{
    [SerializeField] private HeartMovementInvoker _heartMovementInvoker;
    [SerializeField] private SoundData _bumperSound;

    protected override void Awake()
    {
        base.Awake();
        if (_fx == null) _fx = _bumperSound;
        if (_heartMovementInvoker != null)
            _heartMovementInvoker.OnBumperBounced += PlayFX;
    }

    private void OnDestroy()
    {
        if (_heartMovementInvoker != null)
            _heartMovementInvoker.OnBumperBounced -= PlayFX;
    }
}