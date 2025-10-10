using UnityEngine;

namespace Hearts.Movements
{
    using Extensions;
    using Commands;
    
    [RequireComponent(typeof(CharacterController), typeof(Collider))]
    public class HeartMovementInvoker : CommandInvoker
    {
        private const float _boundsRestitution = .8f;

        [SerializeField] private float _despawnTime;
        [SerializeField] private float _bumperBoundsForce;
        [SerializeField] private LayerMask _jumperLayer;

        private float _timerToDespawn = 0;
        private Vector3 _direction;
        private CharacterController _controller;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            ExecuteMovement();

            if (_direction.y < .4f)
                _timerToDespawn += Time.deltaTime;
            else
                _timerToDespawn = 0;

            if (_timerToDespawn >= _despawnTime)
            {
                Destroy(gameObject);
            }
        }

        private void ExecuteMovement()
        {
            _direction.y = gameObject.ApplyGravity(_direction.y, 0);
            
            HeartMovementCommand newCommand = new(_controller, _direction, _boundsRestitution);
            ExecuteCommand(newCommand);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Vector3 hitNormal = hit.normal;
            Vector3 velocity = _direction;
            
            if (_jumperLayer == (_jumperLayer | (1 << hit.gameObject.layer)))
            {
                _direction = velocity - (1 + _bumperBoundsForce) * Vector3.Dot(velocity, hitNormal) * hitNormal;
                _direction.z = 0;
            
                return;
            }
            
            _direction = velocity - (1 + _boundsRestitution) * Vector3.Dot(velocity, hitNormal) * hitNormal;
            _direction.z = 0;
        }
    }
}