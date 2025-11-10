using UnityEngine;
using Hearts;
using Audio;

public class HeartAddedSFX : SFXLauncher
{
    [SerializeField] private HeartBehaviour _heartBehaviour;
    [SerializeField] private SoundData _pickupHeartSound;

    protected override void Awake()
    {
        base.Awake();
        if (_fx == null) _fx = _pickupHeartSound;
        if (_heartBehaviour != null)
            _heartBehaviour.OnTransmittedPoint += PlayFX;
    }

    private void OnDestroy()
    {
        if (_heartBehaviour != null)
            _heartBehaviour.OnTransmittedPoint -= PlayFX;
    }
}