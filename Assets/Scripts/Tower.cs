using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Critter critter;

    [SerializeField] GameObject projectilePrefab;

    protected bool canShoot = true;

    void Start(){
        GetComponent<SpriteRenderer>().sprite = critter.sprite;
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, critter.attackRange, transform.position, 0, 1<<6);
        if(hit && canShoot){
            GameObject projectileObject = Instantiate(projectilePrefab, transform);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.fireSpeed = critter.projectileSpeed;
            projectile.power = critter.AttackPower();
            projectileObject.GetComponent<SpriteRenderer>().sprite = critter.projectileSprite;
            projectile.target = hit.transform;
            canShoot = false;
            Invoke(nameof(CanShootAgain), critter.reloadTime);
        }
    }

    void CanShootAgain(){
        canShoot = true;
    }

    public Critter Recall(){
        Destroy(gameObject);
        return critter;
    }
}
