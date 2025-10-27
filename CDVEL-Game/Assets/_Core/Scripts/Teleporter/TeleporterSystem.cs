using UnityEngine;
using System;

namespace Teleporter
{
    public class TeleporterSystem : ITeleporterSystem
    {
        public event Action OnTeleporterEnter;
        
        private Transform _arrivedTransform;
        public Transform ArrivedTransform => _arrivedTransform;

        public TeleporterSystem(Transform arrivedTransform)
        {
            _arrivedTransform = arrivedTransform;
        }

        public void Teleport(GameObject gameObject)
        {
            OnTeleporterEnter?.Invoke();
            
            if (gameObject.TryGetComponent(out CharacterController controller))
            {
                controller.enabled = false;
                gameObject.transform.position = _arrivedTransform.position;
                controller.enabled = true;
                
                return;
            }
            
            gameObject.transform.position = _arrivedTransform.position;
        }
    }
}