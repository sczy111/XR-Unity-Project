using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handPhysics : MonoBehaviour
{
    public Transform target;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;

        // Rotation
        Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation); // Fixed typo

        rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);
        Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;

        rb.angularVelocity = (rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
    } 
}
