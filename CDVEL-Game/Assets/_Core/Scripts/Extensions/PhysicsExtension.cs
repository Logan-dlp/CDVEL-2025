using UnityEngine;

namespace Extensions
{
    public static class PhysicsExtension
    {
        public static float ApplyGravity(this GameObject gameObject, float currentGravity, float distance, float gravity, float maxFallSpeed = -25f)
        {
            Transform t = gameObject.transform;

            bool isGrounded = Physics.Raycast(t.position, -t.up, out RaycastHit hit, distance + 0.05f)
                              && hit.transform != gameObject.transform;

            if (isGrounded)
                return Mathf.Min(0f, currentGravity);
            
            currentGravity += gravity * Time.fixedDeltaTime;
            return Mathf.Max(currentGravity, maxFallSpeed);
        }

        public static bool IsGrounded(this GameObject gameObject, float distance)
        {
            return Physics.Raycast(gameObject.transform.position, -gameObject.transform.up, out RaycastHit hit,
                       distance) && hit.transform != gameObject.transform;
        }
    }
}