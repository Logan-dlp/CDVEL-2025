using UnityEngine;

namespace Players.Movements
{
    using FX;
    
    public class JostledVFXLauncher : VFXLauncher
    {
        [SerializeField] PlayerMovementInvoker playerMovementInvoker;

        protected override void Awake()
        {
            base.Awake();
            playerMovementInvoker.OnJostled += PlayFX;
        }
    }
}