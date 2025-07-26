using System.Collections;
using UnityEngine;

namespace Spawner
{
    public class HeartSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] _heartPrefab;
        [SerializeField] private Transform[] _spawnPoints;
        
        [SerializeField] private float _minHeartSpawnRate;
        [SerializeField] private float _maxHeartSpawnRate;

        private void Start()
        {
            CreateHeart();
        }

        private void CreateHeart()
        {
            IEnumerator SpawnHeart()
            {
                GameObject instance = _heartPrefab[Random.Range(0, _heartPrefab.Length)];
                Transform position = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
                
                yield return new WaitForSeconds(Random.Range(_minHeartSpawnRate, _maxHeartSpawnRate));
                
                Instantiate(instance, position.position, Quaternion.identity);

                StartCoroutine(SpawnHeart());
            }
            
            StartCoroutine(SpawnHeart());
        }
    }
}