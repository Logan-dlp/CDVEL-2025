using UnityEngine;

namespace Extensions
{
    public static class PhysicsExtension
    {
        public static float ApplyGravity(this GameObject gameObject, float currentGravity, float distance, float gravity)
        {
            return Physics.Raycast(gameObject.transform.position, -gameObject.transform.up, out RaycastHit hit, distance)
                   && hit.transform != gameObject.transform
                ? 0 : currentGravity + gravity * Time.fixedDeltaTime;
        }

        public static bool IsGrounded(this GameObject gameObject, float distance)
        {
            return Physics.Raycast(gameObject.transform.position, -gameObject.transform.up, out RaycastHit hit,
                       distance) && hit.transform != gameObject.transform;
        }
    }
}