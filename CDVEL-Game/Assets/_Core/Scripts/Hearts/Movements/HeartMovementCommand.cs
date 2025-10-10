using UnityEngine;

namespace Hearts.Movements
{
    using Commands;
    
    public class HeartMovementCommand : ICommand
    {
        private CharacterController _controller;
        private Vector3 _direction;
        private float _boundsRestitution;
        
        public HeartMovementCommand(CharacterController controller, 
            Vector3 direction,
            float boundsRestitution)
        {
            _controller = controller;
            _direction = direction;
            _boundsRestitution = boundsRestitution;
        }
        
        public void Execute()
        {
            CollisionFlags flags = _controller.Move(_direction * Time.deltaTime);

            if ((flags & CollisionFlags.Below) != 0)
            {
                if (_direction.y < 0)
                {
                    _direction.y = -_direction.y * _boundsRestitution;
                }
            }
            
            if (Mathf.Abs(_direction.y) < .05f && (flags & CollisionFlags.Below) != 0)
            {
                _direction.y = 0;
            }
        }

        public void Undo()
        {
            CollisionFlags flags = _controller.Move(-_direction * Time.deltaTime);

            if ((flags & CollisionFlags.Below) != 0)
            {
                if (_direction.y < 0)
                {
                    _direction.y = -_direction.y * .8f;
                }
            }
            
            if (Mathf.Abs(_direction.y) < .05f && (flags & CollisionFlags.Below) != 0)
            {
                _direction.y = 0;
            }
        }
    }
}