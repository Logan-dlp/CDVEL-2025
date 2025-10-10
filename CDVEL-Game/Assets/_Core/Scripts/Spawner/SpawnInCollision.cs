using System.Collections;
using UnityEngine;

namespace Spawner
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SpawnInCollision : MonoBehaviour, ISpawner
    {
        [SerializeField] private GameObject[] _prefab;
        [SerializeField] private float _minSpawnRate;
        [SerializeField] private float _maxSpawnRate;
        
        private BoxCollider2D _collider2D;

        private void Awake()
        {
            _collider2D = GetComponent<BoxCollider2D>();
            
            Spawn();
        }

        public void Spawn()
        {
            IEnumerator SpawnHeart()
            {
                GameObject instance = _prefab[Random.Range(0, _prefab.Length)];
                
                float instancePositionX = Random.Range(-_collider2D.bounds.extents.x, _collider2D.bounds.extents.x) + _collider2D.bounds.center.x;
                float instancePositionY = Random.Range(-_collider2D.bounds.extents.y, _collider2D.bounds.extents.y) + _collider2D.bounds.center.y;
                
                Vector3 instancePosition = new Vector3(instancePositionX, instancePositionY, transform.position.z);
                
                yield return new WaitForSeconds(Random.Range(_minSpawnRate, _maxSpawnRate));
                
                Instantiate(instance, instancePosition, Quaternion.identity);

                StartCoroutine(SpawnHeart());
            }
            
            StartCoroutine(SpawnHeart());
        }
    }
}