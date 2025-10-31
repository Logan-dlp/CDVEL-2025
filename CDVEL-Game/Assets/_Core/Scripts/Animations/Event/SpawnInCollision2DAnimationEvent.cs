using UnityEngine;

namespace Animations
{
    using Spawner;

    public class SpawnInCollision2DAnimationEvent : MonoBehaviour
    {
        [SerializeField] private SpawnInCollision2D _spawnInCollision2D;

        [SerializeField] private GameObject[] _prefab;
        [SerializeField] private float _minSpawnRate;
        [SerializeField] private float _maxSpawnRate;

        public void ChangeVariable()
        {
            _spawnInCollision2D.ChangeParameter(_prefab, _minSpawnRate, _maxSpawnRate);
        }

        public void Reset()
        {
            _spawnInCollision2D.ResetParameter();
        }
    }
}
