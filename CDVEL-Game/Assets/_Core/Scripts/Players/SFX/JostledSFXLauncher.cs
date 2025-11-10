using UnityEngine;
using Players;
using Audio;

public class JostledSFXLauncher : SFXLauncher
{
    [SerializeField] private PlayerMovementInvoker _playerMovementInvoker;
    [SerializeField] private SoundData _jostledSound;

    protected override void Awake()
    {
        base.Awake();
        if (_fx == null) _fx = _jostledSound;
        if (_playerMovementInvoker != null)
            _playerMovementInvoker.OnJostled += PlayFX;
    }

    private void OnDestroy()
    {
        if (_playerMovementInvoker != null)
            _playerMovementInvoker.OnJostled -= PlayFX;
    }
}
