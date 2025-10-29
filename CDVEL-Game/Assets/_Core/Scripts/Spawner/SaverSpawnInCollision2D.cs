using System.Collections.Generic;
using UnityEngine;

namespace Spawner
{
    public struct SaverSpawnInCollision2D
    {
        private List<GameObject> _prefabs;
        public List<GameObject> Prefabs => _prefabs;

        private float _minSpawnRate;
        public float MinSpawnRate => _minSpawnRate;

        private float _maxSpawnRate;
        public float MaxSpawnRate => _maxSpawnRate;

        public SaverSpawnInCollision2D(List<GameObject> prefab, float minSpawnRate, float maxSpawnRate)
        {
            this._prefabs = prefab;
            this._minSpawnRate = minSpawnRate;
            this._maxSpawnRate = maxSpawnRate;
        }
    }
}
