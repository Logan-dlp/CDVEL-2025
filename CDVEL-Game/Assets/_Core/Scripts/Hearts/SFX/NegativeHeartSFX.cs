using UnityEngine;
using Hearts;
using Audio;

public class NegativeHeartSFX : SFXLauncher
{
    [SerializeField] private HeartBehaviour _heartBehaviour;
    [SerializeField] private SoundData _NegativeHeartSound;

    protected override void Awake()
    {
        base.Awake();
        if (_fx == null) _fx = _NegativeHeartSound;
        if (_heartBehaviour != null)
            _heartBehaviour.OnNegativePoints += PlayFX;
    }

    private void OnDestroy()
    {
        if (_heartBehaviour != null)
            _heartBehaviour.OnNegativePoints -= PlayFX;
    }
}