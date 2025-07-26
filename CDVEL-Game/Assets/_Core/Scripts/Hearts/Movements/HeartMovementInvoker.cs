using UnityEngine;

namespace Hearts.Movements
{
    using Extensions;
    using Commands;
    
    [RequireComponent(typeof(CharacterController), typeof(Collider))]
    public class HeartMovementInvoker : CommandInvoker
    {
        [SerializeField] private LayerMask _jumperLayer;
        [SerializeField] private float _gravityDetectionDistance;

        private Vector3 _direction;
        private CharacterController _controller;

        private bool test = true;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void FixedUpdate()
        {
            ExecuteMovement();
        }

        private void ExecuteMovement()
        {
            _direction.y = gameObject.ApplyGravity(_direction.y, _gravityDetectionDistance);
            
            if (_direction.x > 0)
            {
                if (_direction.x - Time.fixedDeltaTime < 0)
                {
                    _direction.x = 0;
                }
                else
                {
                    _direction.x -= Time.fixedDeltaTime;
                }
            }
            
            if (_direction.x < 0)
            {
                if (_direction.x + Time.fixedDeltaTime > 0)
                {
                    _direction.x = 0;
                }
                else
                {
                    _direction.x += Time.fixedDeltaTime;
                }
            }

            if (test && _direction.y == 0)
            {
                _direction.x = 0;
            }

            if (_direction.y != 0)
            {
                test = true;
            }

            if (_direction == Vector3.zero)
                return;
            
            HeartMovementCommand newCommand = new HeartMovementCommand(gameObject, _controller, _direction);
            ExecuteCommand(newCommand);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_jumperLayer == (_jumperLayer | (1 << collision.collider.gameObject.layer)))
            {
                test = false;
                Vector3 jumpedDirection = transform.position - 2 * collision.contacts[0].normal * Vector3.Dot(collision.contacts[0].normal, transform.position);
                _direction = new Vector3(-jumpedDirection.x, jumpedDirection.y, _direction.z);
            }
        }
    }
}