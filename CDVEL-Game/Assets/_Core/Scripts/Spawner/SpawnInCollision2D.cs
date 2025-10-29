using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Spawner
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SpawnInCollision2D : MonoBehaviour, ISpawner
    {
        [SerializeField] private List<GameObject> _prefab;
        [SerializeField] private float _minDistanceBetweenSpawn;
        [SerializeField] private float _minSpawnRate;
        [SerializeField] private float _maxSpawnRate;

        private SaverSpawnInCollision2D _saverSpawnInCollision2D;
        private BoxCollider2D _collider2D;
        private Vector3 _beforeInstancePosition;

        private void Awake()
        {
            _collider2D = GetComponent<BoxCollider2D>();
            _saverSpawnInCollision2D = new(_prefab, _minSpawnRate, _maxSpawnRate);

            Spawn();
        }

        public void ChangeParameter(GameObject[] prefab, float minSpawnRate, float maxSpawnRate)
        {
            _prefab.Clear();

            foreach (GameObject prefabIterator in prefab)
            {
                _prefab.Add(prefabIterator);
            }

            _minSpawnRate = minSpawnRate;
            _maxSpawnRate = maxSpawnRate;
        }

        public void ResetParameter()
        {
            _prefab = _saverSpawnInCollision2D.Prefabs;
            _minSpawnRate = _saverSpawnInCollision2D.MinSpawnRate;
            _maxSpawnRate = _saverSpawnInCollision2D.MaxSpawnRate;
        }

        public void Spawn()
        {
            StartCoroutine(SpawnHeartCoroutine());
        }

        private IEnumerator SpawnHeartCoroutine()
        {
            GameObject instance = _prefab[Random.Range(0, _prefab.Count)];
            Vector3 instancePosition = GenerateRandomPosition(_collider2D);

            while (Mathf.Abs(instancePosition.x - _beforeInstancePosition.x) < _minDistanceBetweenSpawn)
            {
                instancePosition = GenerateRandomPosition(_collider2D);
            }

            yield return new WaitForSeconds(Random.Range(_minSpawnRate, _maxSpawnRate));

            Instantiate(instance, instancePosition, Quaternion.identity);
            _beforeInstancePosition = instancePosition;

            StartCoroutine(SpawnHeartCoroutine());
        }

        private Vector3 GenerateRandomPosition(Collider2D collider2D)
        {
            float instancePositionX = Random.Range(-collider2D.bounds.extents.x, collider2D.bounds.extents.x) + collider2D.bounds.center.x;
            float instancePositionY = Random.Range(-collider2D.bounds.extents.y, collider2D.bounds.extents.y) + collider2D.bounds.center.y;

            Vector3 instancePosition = new Vector3(instancePositionX, instancePositionY, transform.position.z);

            return instancePosition;
        }
    }
}
