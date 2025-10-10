using UnityEngine.InputSystem;
using UnityEngine;

namespace Players.Movements
{
    using Extensions;
    using Commands;
    
    [RequireComponent(typeof(CharacterController), typeof(Collider))]
    public class PlayerMovementInvoker : CommandInvoker
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _gravityDetectionDistance;

        private bool _isJumped;
        private bool _isGrounded;
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

        public void SetDirection(InputAction.CallbackContext ctx) => _direction = new Vector3(ctx.ReadValue<Vector2>().x, _direction.y, 0);

        public void ExecuteJump(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                _isJumped = _isGrounded;
            }
        }

        private void ExecuteMovement()
        {
            _direction.y = gameObject.ApplyGravity(_direction.y, _gravityDetectionDistance);
            _isGrounded = gameObject.IsGrounded(_gravityDetectionDistance);
            
            if (_isJumped)
            {
                _direction = new Vector3(_direction.x, Mathf.Sqrt(_jumpForce * -2f * Physics.gravity.y), 0);
                _isJumped = false;
            }

            if (_direction == Vector3.zero)
                return;
            
            PlayerMovementCommand newCommand = new PlayerMovementCommand(gameObject, _controller, _direction, _speed);
            ExecuteCommand(newCommand);
        }
    }
}