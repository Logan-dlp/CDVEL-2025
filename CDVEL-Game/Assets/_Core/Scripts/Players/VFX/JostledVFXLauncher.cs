using UnityEngine;

namespace Players
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