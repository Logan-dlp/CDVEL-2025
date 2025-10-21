using UnityEngine;

namespace Players.Movements
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationMovement : MonoBehaviour
    {
        [SerializeField] private PlayerMovementInvoker _playerMovementInvoker;
        
        private Animator _animator;

        private void OnEnable()
        {
            _playerMovementInvoker.OnVelocityChange += SetAnimationMovement;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnDestroy()
        {
            _playerMovementInvoker.OnVelocityChange -= SetAnimationMovement;
        }

        private void SetAnimationMovement(Vector3 velocity, float speed)
        {
            if (velocity.y != 0)
                _animator.SetBool("IsJumped", true);
            else
                _animator.SetBool("IsJumped", false);
            
            
            _animator.SetFloat("Velocity", speed);
        }
    }
}