using UnityEngine;

namespace Hearts
{
    using FX;
    
    public class AddedPointVFX : ParticleLauncher
    {
        [SerializeField] private HeartBehaviour _heartBehaviour;

        protected override void Awake()
        {
            base.Awake();
            _heartBehaviour.OnTransmittedPoint += PlayFX;
        }
    }
}