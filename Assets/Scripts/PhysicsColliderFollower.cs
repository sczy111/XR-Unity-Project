using UnityEngine;

public class PhysicsColliderFollower : MonoBehaviour
{
    public Transform targetHand; // The animated hand to follow
    private Rigidbody rb;
    public float positionForce = 500f; // Strength of force applied for movement
    public float rotationForce = 100f; // Strength of torque applied for rotation

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.mass = 1f; // Adjust as needed
        rb.drag = 5f; // Prevents excessive floating
        rb.angularDrag = 5f; // Prevents excessive spinning
    }

    void FixedUpdate()
    {
        if (targetHand == null) return;

        // Calculate velocity required to follow the hand
        Vector3 velocity = (targetHand.position - rb.position) / Time.fixedDeltaTime;
        rb.velocity = Vector3.ClampMagnitude(velocity, positionForce * Time.fixedDeltaTime);

        // Calculate rotation difference
        Quaternion rotationDifference = targetHand.rotation * Quaternion.Inverse(rb.rotation);
        rotationDifference.ToAngleAxis(out float angle, out Vector3 axis);
        if (angle > 180f) angle -= 360f;
        Vector3 angularVelocity = (axis * angle * Mathf.Deg2Rad) / Time.fixedDeltaTime;

        // Apply rotation force
        rb.angularVelocity = Vector3.ClampMagnitude(angularVelocity, rotationForce * Time.fixedDeltaTime);
    }
}
