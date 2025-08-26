using UnityEngine;

namespace Teleporter
{
    public interface ITeleporterSystem
    {
        public Transform ArrivedTransform { get; }
        public void Teleport(GameObject gameObject);
    }
}