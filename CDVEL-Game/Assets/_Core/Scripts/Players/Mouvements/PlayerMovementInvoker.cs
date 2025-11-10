using UnityEngine.InputSystem;
using UnityEngine;
using System;

namespace Players
{
    using Extensions;
    using Commands;
    
    [RequireComponent(typeof(CharacterController), typeof(Collider))]
    public class PlayerMovementInvoker : CommandInvoker
    {
        /// <summary>
        /// Sends information when it has been hit.
        /// <returns>- Vector 3</returns>
        /// </summary>
        public event Action<Vector3> OnHaveJostled;

        public event Action OnJostled;
        public event Action<Vector3, float> OnVelocityChange; 
        
        [SerializeField] private float _speed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _jostledForce;
        [SerializeField] private float _gravityDetectionDistance;

        private bool _isJumped;
        private bool _isGrounded;
        private bool _isJostled;
        private bool _isActiveInput;
        
        private float _currentSpeed;
        private float _skinRotationOffset;
        
        private Vector3 _velocity;
        private Vector3 _jostledVelocity;
        
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

            OnHaveJostled += ExecuteJostled;
            _skinRotationOffset = _skin.rotation.y;
        }

        private void Update()
        {
            ExecuteMovement();
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!hit.transform.TryGetComponent(out PlayerMovementInvoker hitMovementInvoker))
                return;
            
            if (hit.transform == transform)
                return;

            ExecuteJostled(hit.transform.position);
            hitMovementInvoker.OnHaveJostled?.Invoke(transform.position);
        }

        public void SetDirection(InputAction.CallbackContext ctx)
        {
            if (_isActiveInput && Time.timeScale > 0)
            {
                _velocity = new Vector3(ctx.ReadValue<Vector2>().x, _velocity.y, 0);

                if (Mathf.Abs(_velocity.x) >= .15f)
                {
                    int direction = (int)Mathf.Sign(_velocity.x);
                    _skin.rotation = Quaternion.Euler(0, _skinRotationOffset + (90 * direction), 0);
                }
            }

            if (ctx.canceled)
                _currentSpeed = 0;
        }

        public void ExecuteJump(InputAction.CallbackContext ctx)
        {
            if (ctx.started && Time.timeScale > 0)
            {
                _isJumped = _isGrounded;
            }
        }

        private void ExecuteJostled(Vector3 hit)
        {
            if (_isJostled)
                return;
            
            _jostledVelocity.x = transform.position.x - hit.x;
            _isJostled = true;
            OnJostled?.Invoke();
        }

        private void ExecuteMovement()
        {
            if (Time.timeScale <= 0)
                return;
            
            _velocity.y = gameObject.ApplyGravity(_velocity.y, _gravityDetectionDistance, Physics.gravity.y);
            _isGrounded = gameObject.IsGrounded(_gravityDetectionDistance);

            if (!_isActiveInput && _isGrounded)
            {
                _isActiveInput = true;
                _velocity.x = 0;
                _currentSpeed = 0;
            }
            
            if (_velocity.x != 0)
                if (_currentSpeed < _speed)
                    _currentSpeed = Mathf.Min(_currentSpeed + Time.deltaTime * _acceleration, _speed);
            
            if (_isJumped)
            {
                _velocity = new Vector3(_velocity.x, Mathf.Sqrt(_jumpForce * -2f * Physics.gravity.y), 0);
                _isJumped = false;
            }
            
            if (_isJostled)
            {
                _velocity = new Vector3(Mathf.Sign(_jostledVelocity.x) * _jostledForce, Mathf.Sqrt(_jumpForce * -2f * Physics.gravity.y), 0);
                _isJostled = false;
                _isActiveInput = false;
            }

            OnVelocityChange?.Invoke(_velocity, _currentSpeed/_speed);
            
            if (_velocity == Vector3.zero)
                return;
            
            PlayerMovementCommand newCommand = new PlayerMovementCommand(gameObject, _controller, _velocity, _currentSpeed);
            ExecuteCommand(newCommand);
        }
    }
}