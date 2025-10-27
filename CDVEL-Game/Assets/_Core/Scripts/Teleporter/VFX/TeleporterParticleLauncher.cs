using UnityEngine;

namespace Teleporter
{
    using FX;
    
    public class TeleporterParticleLauncher : ParticleLauncher
    {
        [SerializeField] private TeleporterBehaviour _teleporterBehaviour;
        
        protected override void Awake()
        {
            base.Awake();
            _teleporterBehaviour.OnTeleporterEnter += PlayFX;
        }

        private void OnDestroy()
        {
            _teleporterBehaviour.OnTeleporterEnter -= PlayFX;
        }
    }
}