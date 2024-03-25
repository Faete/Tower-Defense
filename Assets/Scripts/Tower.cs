using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public float range;
    public float reloadTime;
    [SerializeField] GameObject projectilePrefab;


    public float projectileSpeed;
    public float projectilePower;
    public Sprite projectileSprite;
    protected bool canShoot = true;

    void Update()
    {
       RaycastHit2D hit = Physics2D.CircleCast(transform.position, range, transform.position, 0, 1<<6);
       if(hit && canShoot){
        GameObject projectileObject = Instantiate(projectilePrefab, transform);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.fireSpeed = projectileSpeed;
        projectile.power = projectilePower;
        projectileObject.GetComponent<SpriteRenderer>().sprite = projectileSprite;
        projectile.target = hit.transform;
        canShoot = false;
        Invoke(nameof(CanShootAgain), reloadTime);
       }
    }

    void CanShootAgain(){
        canShoot = true;
    }
}
