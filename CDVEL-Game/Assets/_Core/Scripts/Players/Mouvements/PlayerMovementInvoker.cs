using UnityEngine.InputSystem;
using UnityEngine;

namespace Players.Mouvements
{
    using Extensions;
    using Commands;
    
    [RequireComponent(typeof(CharacterController), typeof(Collider))]
    public class PlayerMovementInvoker : CommandInvoker
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _gravityDetectionDistance;

        private Vector3 _direction;
        private CharacterController _controller;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void FixedUpdate()
        {
            ExecuteMovement();
        }

        public void SetDirection(InputAction.CallbackContext ctx) => _direction = new Vector3(ctx.ReadValue<Vector2>().x, 0, 0);

        private void ExecuteMovement()
        {
            _direction.y = gameObject.ApplyGravity(_direction.y, _gravityDetectionDistance);

            if (_direction == Vector3.zero)
                return;
            
            PlayerMovementCommand newCommand = new PlayerMovementCommand(gameObject, _controller, _direction, _speed);
            ExecuteCommand(newCommand);
        }
    }
}