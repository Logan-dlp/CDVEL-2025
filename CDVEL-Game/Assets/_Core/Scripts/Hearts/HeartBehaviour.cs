using UnityEngine;
using System;

namespace Hearts
{
    using Scores;
    
    public class HeartBehaviour : MonoBehaviour
    {
        public event Action OnTransmittedPoint;

        [SerializeField] private LayerMask _unspawnLayer;
        
        [SerializeField] private int _points;
        private bool _enableCollider = true;

        private void OnCollisionEnter(Collision collision)
        {
            if (_enableCollider)
            {
                if (collision.transform.TryGetComponent<ScoreHandler>(out var scoreHandler))
                {
                    _enableCollider = false;
                    scoreHandler.OnAddedPoints(_points);
                    OnTransmittedPoint?.Invoke();
                    Destroy(gameObject);
                }

                if (_unspawnLayer == (_unspawnLayer | (1 << collision.gameObject.layer)))
                {
                    _enableCollider = false;
                    Destroy(gameObject);
                }
            }
        }
    }
}
