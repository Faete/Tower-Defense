using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float fireSpeed = 2f;
    public float power = 3f;
    public Transform target;

    void FixedUpdate()
    {
        if(target) transform.position = Vector3.MoveTowards(transform.position, target.position, fireSpeed);
        else Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy") Destroy(gameObject);
    }
}
