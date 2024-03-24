using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float fireSpeed = 2f;
    public float power = 3f;
    public Transform target;

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, fireSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy") Destroy(gameObject);
    }
}
