using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float fireSpeed;
    public float power;
    public Transform target;

    void FixedUpdate()
    {
        if(target) transform.position = Vector3.MoveTowards(transform.position, target.position, fireSpeed);
        else Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy") || other.CompareTag("Boss")) Destroy(gameObject);
    }
}
