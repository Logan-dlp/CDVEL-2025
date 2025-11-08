using System;
using System.Collections;
using UnityEngine;

namespace FX
{
    public class ColorPulser : MonoBehaviour
    {
        private const string COLOR_PROPERTY = "_BumperOulineColor";

        [SerializeField] private float _pulseMultiplier;
        [SerializeField] private float _pulseSpeed;
        [SerializeField] private float _pulseCount;

        private Material _materialInstance;
        private Color _pulseColor;

        private void Awake()
        {
            _materialInstance = GetComponent<Renderer>().material;
            _pulseColor = _materialInstance.GetColor(COLOR_PROPERTY);
        }

        public void StartPulsing()
        {
            IEnumerator PulseRoutine()
            {
                float totalDuration = _pulseCount * Mathf.PI;
                float time = 0f;

                while (time < totalDuration)
                {
                    float t = Mathf.Sin(time * _pulseSpeed);
                    float intensity = Mathf.Lerp(1f, _pulseMultiplier, t);
                    _materialInstance.SetColor(COLOR_PROPERTY, _pulseColor * intensity);
                    
                    time += Time.deltaTime * _pulseSpeed;
                    yield return null;
                }
                
                _materialInstance.SetColor(COLOR_PROPERTY, _pulseColor);
            }
            
            StopAllCoroutines();
            StartCoroutine(PulseRoutine());
        }
    }
}