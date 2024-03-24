using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField] float range = 2f;
    [SerializeField] float reloadTime = 0.5f;
    [SerializeField] GameObject projectilePrefab;

    protected bool canShoot = true;

    void Update()
    {
       // Use CircleCast to see if an enemy is in range
       RaycastHit2D hit = Physics2D.CircleCast(transform.position, range, transform.position, 0, 1<<6);
       if(hit && canShoot){
        GameObject projectile = Instantiate(projectilePrefab, transform);
        projectile.GetComponent<Projectile>().target = hit.transform;
        canShoot = false;
        Invoke(nameof(CanShootAgain), reloadTime);
       }
    }

    void CanShootAgain(){
        canShoot = true;
    }
}
