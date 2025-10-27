using System.Collections;
using UnityEngine;
using System;

namespace Hearts
{
    using Scores;
    
    public class HeartBehaviour : MonoBehaviour
    {
        public event Action OnTransmittedPoint;
        public event Action OnUndoBehaviour;
        
        [SerializeField] private LayerMask _unspawnLayer;
        [SerializeField] private float _timeToMove;
        [SerializeField] private int _points;
        
        private bool _enableCollider = true;

        private void OnEnable()
        {
            var movementInvoker = GetComponent<HeartMovementInvoker>();
            movementInvoker.enabled = false; 
            IEnumerator EnableMovement(float timeToEnable)
            {
                yield return new WaitForSeconds(timeToEnable);
                movementInvoker.enabled = true;
            }

            StartCoroutine(EnableMovement(_timeToMove));
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (_enableCollider)
            {
                if (hit.transform.TryGetComponent<ScoreHandler>(out var scoreHandler))
                {
                    _enableCollider = false;
                    scoreHandler.OnAddedPoints(_points);
                    OnTransmittedPoint?.Invoke();
                    Destroy(gameObject);
                }

                if (_unspawnLayer == (_unspawnLayer | (1 << hit.gameObject.layer)))
                {
                    _enableCollider = false;
                    OnUndoBehaviour?.Invoke();
                    Destroy(gameObject);
                }
            }
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}
