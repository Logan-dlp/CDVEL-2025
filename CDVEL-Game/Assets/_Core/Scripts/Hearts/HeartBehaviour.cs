using UnityEngine;
using System;

namespace Hearts
{
    using Scores;
    
    public class HeartBehaviour : MonoBehaviour
    {
        public event Action OnTransmittedPoint;
        
        [SerializeField] private int _points;
        private bool _enableCollider = true;

        private void OnCollisionEnter(Collision collision)
        {
            if (_enableCollider)
            {
                if (collision.transform.TryGetComponent<ScoreHandler>(out var scoreHandler))
                {
                    _enableCollider  = false;
                    scoreHandler.OnAddedPoints(_points);
                    OnTransmittedPoint?.Invoke();
                    Destroy(gameObject);
                }
                
            }
        }
    }
}
