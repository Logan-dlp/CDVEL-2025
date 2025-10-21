using UnityEngine.InputSystem;
using UnityEngine;
using System;

namespace Players.Movements
{
    using Extensions;
    using Commands;
    
    [RequireComponent(typeof(CharacterController), typeof(Collider))]
    public class PlayerMovementInvoker : CommandInvoker
    {
        public event Action<Vector3, float> OnVelocityChange; 
        
        [SerializeField] private float _speed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _gravityDetectionDistance;

        private bool _isJumped;
        private bool _isGrounded;
        
        private float _currentSpeed;
        private float _skinRotationOffset;
        
        private Vector3 _velocity;
        
        private Transform _skin;
        private CharacterController _controller;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            
            foreach (Transform child in transform)
            {
                if (child.tag == "Skin")
                    _skin = child;
                break;
            }
            
            _skinRotationOffset = _skin.rotation.y;
        }

        private void Update()
        {
            ExecuteMovement();
        }

        public void SetDirection(InputAction.CallbackContext ctx)
        {
            _velocity = new Vector3(ctx.ReadValue<Vector2>().x, _velocity.y, 0);

            if (Mathf.Abs(_velocity.x) >= .75f)
            {
                int direction = (int)Mathf.Sign(_velocity.x);
                _skin.rotation = Quaternion.Euler(0, _skinRotationOffset + (90 * direction), 0);
            }

            if (ctx.canceled)
                _currentSpeed = 0;
        }

        public void ExecuteJump(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                _isJumped = _isGrounded;
            }
        }

        private void ExecuteMovement()
        {
            _velocity.y = gameObject.ApplyGravity(_velocity.y, _gravityDetectionDistance, Physics.gravity.y);
            _isGrounded = gameObject.IsGrounded(_gravityDetectionDistance);
            
            if (_velocity.x != 0)
                if (_currentSpeed < _speed)
                    _currentSpeed = Mathf.Min(_currentSpeed + Time.deltaTime * _acceleration, _speed);
            
            if (_isJumped)
            {
                _velocity = new Vector3(_velocity.x, Mathf.Sqrt(_jumpForce * -2f * Physics.gravity.y), 0);
                _isJumped = false;
            }
            
            OnVelocityChange?.Invoke(_velocity, _currentSpeed/_speed);

            if (_velocity == Vector3.zero)
                return;
            
            PlayerMovementCommand newCommand = new PlayerMovementCommand(gameObject, _controller, _velocity, _currentSpeed);
            ExecuteCommand(newCommand);
        }
    }
}