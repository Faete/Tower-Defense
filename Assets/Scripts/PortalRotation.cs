using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalRotation : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 8f;

    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * -1);
    }
}
