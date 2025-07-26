using UnityEngine;

namespace Hearts.Movements
{
    using Commands;
    
    public class HeartMovementCommand : ICommand
    {
        private GameObject _gameObject;
        private CharacterController _controller;
        private Vector3 _direction;
        
        public HeartMovementCommand(GameObject gameObject, 
            CharacterController controller, 
            Vector3 direction)
        {
            _gameObject = gameObject;
            _controller = controller;
            _direction = direction;
        }
        
        public void Execute()
        {
            Vector3 currentMovement = _gameObject.transform.right * _direction.x
                                      + _gameObject.transform.up * _direction.y;
            
            _controller.Move(currentMovement * Time.fixedDeltaTime);
        }

        public void Undo()
        {
            Vector3 currentMovement = _gameObject.transform.right * _direction.x
                                      + _gameObject.transform.up * _direction.y;
            
            _controller.Move(-currentMovement * Time.fixedDeltaTime);
        }
    }
}