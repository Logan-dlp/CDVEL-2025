using UnityEngine;

namespace Players.Movements
{
    using Commands;
    
    public class PlayerMovementCommand : ICommand
    {
        private GameObject _gameObject;
        private CharacterController _controller;
        private Vector3 _direction;
        private float _speed;
        
        public PlayerMovementCommand(GameObject gameObject, 
                                    CharacterController controller, 
                                    Vector3 direction, 
                                    float speed)
        {
            _gameObject = gameObject;
            _controller = controller;
            _direction = direction;
            _speed = speed;
        }
        
        public void Execute()
        {
            Vector3 currentMovement = _gameObject.transform.right * (_direction.x * _speed)
                                        + _gameObject.transform.up * _direction.y;
            
            _controller.Move(currentMovement * Time.fixedDeltaTime);
        }

        public void Undo()
        {
            Vector3 currentMovement = _gameObject.transform.right * (_direction.x * _speed)
                                      + _gameObject.transform.up * _direction.y;
            
            _controller.Move(-currentMovement * Time.fixedDeltaTime);
        }
    }
}