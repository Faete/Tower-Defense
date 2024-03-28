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
            projectile.power = critter.baseAttack * Mathf.Pow(1.05f, critter.level);
            projectileObject.GetComponent<SpriteRenderer>().sprite = critter.projectileSprite;
            projectile.target = hit.transform;
            canShoot = false;
            Invoke(nameof(CanShootAgain), critter.reloadTime);
        }
    }

    void CanShootAgain(){
        canShoot = true;
    }

    public void GainExperience(int exp){
        Debug.Log($"{critter.name} gained {exp} experience");
        if(critter.experience >= critter.experienceToLevel){
            critter.level++;
            critter.experience -= critter.experienceToLevel;
            critter.experienceToLevel += 100;
        }
    }
}
