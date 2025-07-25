using UnityEngine;
using System;

namespace Hearts
{
    using Scores;
    
    public class HeartBehaviour : MonoBehaviour
    {
        public event Action OnTransmittedPoint;
        
        [SerializeField] private int _points;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent<ScoreHandler>(out var scoreHandler))
            {
                scoreHandler.OnAddedPoints(_points);
                OnTransmittedPoint?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
