using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
    

public class Enemy : MonoBehaviour
{
    public Transform path;

    protected List<Vector3> pathPoints = new List<Vector3>();
    protected int pathPointIndex = 0;

    public Critter critter;
    float currentHealth;
    Transform healthBarFill;
    public bool canCatch;

    void Start()
    {
        healthBarFill = transform.GetChild(0);
        GetComponent<SpriteRenderer>().sprite = critter.sprite;
        currentHealth = critter.health;
        foreach(Transform point in path){
            pathPoints.Add(point.position);
        }
        transform.position = pathPoints[0];
        canCatch = false;
    }

    void Update()
    {
        Movement();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Projectile"))
        {
            currentHealth -= other.gameObject.GetComponent<Projectile>().power;
            if(currentHealth < (critter.health / 2)) canCatch = true;
        }
    }
    
    void Movement(){
        if(currentHealth <= 0) Destroy(gameObject);
        healthBarFill.localScale = new Vector3(currentHealth / critter.health, 0.15f, 1f);

        if((pathPointIndex + 1) >= pathPoints.Count) Destroy(gameObject);
        else{
            transform.position = Vector3.MoveTowards(transform.position, pathPoints[pathPointIndex + 1], critter.moveSpeed * Time.deltaTime);
            if(transform.position == pathPoints[pathPointIndex + 1]) pathPointIndex++;
        }
    }
}
