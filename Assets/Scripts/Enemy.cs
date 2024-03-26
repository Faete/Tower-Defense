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
    float maxHealth;
    float moveSpeed;
    float currentHealth;
    [SerializeField] Transform healthBarFill;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = critter.sprite;
        maxHealth = critter.health;
        moveSpeed = critter.moveSpeed;
        currentHealth = maxHealth;
        foreach(Transform point in path){
            pathPoints.Add(point.position);
        }
        transform.position = pathPoints[0];
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
        }
    }
    
    void Movement(){
        if(currentHealth <= 0) Destroy(gameObject);
        healthBarFill.localScale = new Vector3(currentHealth / maxHealth, 0.15f, 1f);

        if((pathPointIndex + 1) >= pathPoints.Count) Destroy(gameObject);
        else{
            transform.position = Vector3.MoveTowards(transform.position, pathPoints[pathPointIndex + 1], moveSpeed * Time.deltaTime);
            if(transform.position == pathPoints[pathPointIndex + 1]) pathPointIndex++;
        }
    }
}
