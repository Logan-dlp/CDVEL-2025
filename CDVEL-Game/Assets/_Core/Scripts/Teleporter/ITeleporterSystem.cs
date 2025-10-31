using UnityEngine;
using System;

namespace Teleporter
{
    public interface ITeleporterSystem
    {
        public event Action OnTeleporterEnter;
        public Transform ArrivedTransform { get; }
        public void Teleport(GameObject gameObject);
    }
}