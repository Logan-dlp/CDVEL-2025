using UnityEngine;
using Teleporter;
using Audio;

public class TeleporterSFXLauncher : SFXLauncher
{
    [SerializeField] private TeleporterBehaviour _teleporterBehaviour;
    [SerializeField] private SoundData _teleporterSound;

    protected override void Awake()
    {
        base.Awake();
        if (_fx == null) _fx = _teleporterSound;
        if (_teleporterBehaviour != null)
            _teleporterBehaviour.OnTeleporterEnter += PlayFX;
    }

    private void OnDestroy()
    {
        if (_teleporterBehaviour != null)
            _teleporterBehaviour.OnTeleporterEnter -= PlayFX;
    }
}
