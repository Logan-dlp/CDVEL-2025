using UnityEngine;
using Hearts;
using Audio;

public class DestroyHeartSFX : SFXLauncher
{
    [SerializeField] private HeartBehaviour heartBehaviour;
    [SerializeField] private SoundData undoSound;

    protected override void Awake()
    {
        base.Awake();
        if (_fx == null) _fx = undoSound;
        if (heartBehaviour != null)
            heartBehaviour.OnUndoBehaviour += PlayFX;
    }

    private void OnDestroy()
    {
        if (heartBehaviour != null)
            heartBehaviour.OnUndoBehaviour -= PlayFX;
    }
}
