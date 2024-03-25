using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tower : MonoBehaviour
{

    float range;
    float reloadTime;
    [SerializeField] GameObject projectilePrefab;

    protected bool canShoot = true;

    public Critter critter;

    void Start(){
        range = critter.attackRange;
        reloadTime = critter.reloadTime;
        GetComponent<SpriteRenderer>().sprite = critter.sprite;
    }

    void Update()
    {
       RaycastHit2D hit = Physics2D.CircleCast(transform.position, range, transform.position, 0, 1<<6);
       if(hit && canShoot){
        GameObject projectileObject = Instantiate(projectilePrefab, transform);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.fireSpeed = critter.projectileSpeed;
        projectile.power = critter.attackPower;
        projectileObject.GetComponent<SpriteRenderer>().sprite = critter.projectileSprite;
        projectile.target = hit.transform;
        canShoot = false;
        Invoke(nameof(CanShootAgain), reloadTime);
       }
    }

    void CanShootAgain(){
        canShoot = true;
    }
}
