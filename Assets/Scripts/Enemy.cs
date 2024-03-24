using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
    

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Transform path;

    protected List<Vector3> pathPoints = new List<Vector3>();
    protected int pathPointIndex = 0;

    float maxHealth = 5;
    float currentHealth;
    [SerializeField] Transform healthBarFill;

    void Start()
    {
        currentHealth = maxHealth;
        foreach(Transform point in path){
            pathPoints.Add(point.position);
        }
        transform.position = pathPoints[0];
    }

    void Update()
    {
        if(currentHealth <= 0) Destroy(gameObject);
        healthBarFill.localScale = new Vector3(currentHealth / maxHealth, 0.15f, 1f);

        if((pathPointIndex + 1) >= pathPoints.Count) Destroy(gameObject);
        transform.position = Vector3.MoveTowards(transform.position, pathPoints[pathPointIndex + 1], moveSpeed * Time.deltaTime);
        if(transform.position == pathPoints[pathPointIndex + 1]) pathPointIndex++;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Projectile"){
            currentHealth -= other.gameObject.GetComponent<Projectile>().power;
        }
    }
}
