using UnityEngine;

namespace Extensions
{
    public static class GameObjectExtension
    {
        public static float ApplyGravity(this GameObject gameObject, float currentGravity, float distance)
        {
            return Physics.Raycast(gameObject.transform.position, -gameObject.transform.up, out RaycastHit hit, distance)
                   && hit.transform != gameObject.transform
                ? 0 : currentGravity + Physics.gravity.y * Time.fixedDeltaTime;
        }
    }
}