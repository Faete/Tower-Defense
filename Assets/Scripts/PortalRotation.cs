using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalRotation : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 8f;

    void FixedUpdate()
    {
        transform.Rotate(-1 * rotationSpeed * Vector3.forward);
    }
}
