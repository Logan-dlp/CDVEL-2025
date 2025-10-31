using UnityEngine;

namespace Hearts
{
    using FX;
    
    public class UndoBehaviourVFX : ParticleLauncher
    {
        [SerializeField] private HeartBehaviour _heartBehaviour;

        protected override void Awake()
        {
            base.Awake();
            _heartBehaviour.OnUndoBehaviour += PlayFX;
        }
    }
}