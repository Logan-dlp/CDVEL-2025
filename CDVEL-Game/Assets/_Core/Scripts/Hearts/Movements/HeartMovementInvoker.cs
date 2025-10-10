using UnityEngine;

namespace Hearts.Movements
{
    using Extensions;
    using Commands;
    
    [RequireComponent(typeof(CharacterController), typeof(Collider))]
    public class HeartMovementInvoker : CommandInvoker
    {
        private const float _boundsRestitution = .8f;
        
        [SerializeField] private LayerMask _jumperLayer;

        private Vector3 _direction;
        private CharacterController _controller;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            ExecuteMovement();
        }

        private void ExecuteMovement()
        {
            _direction.y = gameObject.ApplyGravity(_direction.y, 0);
            
            HeartMovementCommand newCommand = new HeartMovementCommand(_controller, _direction, _boundsRestitution);
            ExecuteCommand(newCommand);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (_jumperLayer == (_jumperLayer | (1 << hit.gameObject.layer)))
            {
                Vector3 hitNormal = hit.normal;
                Vector3 velocity = _direction;
            
                _direction = velocity - (1 + _boundsRestitution) * Vector3.Dot(velocity, hitNormal) * hitNormal;
                _direction.z = 0;
            }
        }
    }
}