using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsProxySync : MonoBehaviour
{
    [Tooltip("The animated finger or joint that this proxy should follow")]
    public Transform visualTarget;

    void LateUpdate()
    {
        if (visualTarget != null)
        {
            transform.position = visualTarget.position;
            transform.rotation = visualTarget.rotation;
        }
    }
}
