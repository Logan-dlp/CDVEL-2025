using UnityEngine;

namespace Teleporter
{
    [RequireComponent(typeof(Collider))]
    public class TeleporterBehaviour : MonoBehaviour
    {
        [SerializeField] private LayerMask _teleportableMask;
        
        private Transform _arrivedTransform;
        private ITeleporterSystem _teleporterSystem;

        private void Awake()
        {
            if (transform.childCount == 0)
            {
                Debug.LogWarning("TeleporterBehaviour needs at least one arrived transform");
                return;
            }
            
            _arrivedTransform = transform.GetChild(0);

            _teleporterSystem = new TeleporterSystem(_arrivedTransform);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (_teleportableMask == (_teleportableMask | (1 << collision.gameObject.layer)) && _teleporterSystem != null)
            {
                _teleporterSystem.Teleport(collision.gameObject);
            }
        }
    }
}