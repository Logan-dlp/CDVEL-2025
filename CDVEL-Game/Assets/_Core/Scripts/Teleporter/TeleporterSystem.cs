using UnityEngine;

namespace Teleporter
{
    public class TeleporterSystem : ITeleporterSystem
    {
        private Transform _arrivedTransform;
        public Transform ArrivedTransform => _arrivedTransform;

        public TeleporterSystem(Transform arrivedTransform)
        {
            _arrivedTransform = arrivedTransform;
        }

        public void Teleport(GameObject gameObject)
        {
            CharacterController characterController = new();
            
            if (gameObject.TryGetComponent<CharacterController>(out CharacterController controller))
            {
                characterController = controller;
            }

            characterController.enabled = false;
            gameObject.transform.position = _arrivedTransform.position;
            characterController.enabled = true;
        }
    }
}