using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
    

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] Transform path;

    protected List<Vector3> pathPoints = new List<Vector3>();
    protected int pathPointIndex = 0;
    protected Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        foreach(Transform point in path){
            pathPoints.Add(point.position);
        }
        transform.position = pathPoints[0];
    }

    void FixedUpdate()
    {
        if(pathPointIndex >= pathPoints.Count){
            Debug.Log("Last point");
            return;
        }
        Vector3 direction = (pathPoints[pathPointIndex + 1] - pathPoints[pathPointIndex]).normalized;
        rb.velocity = direction * moveSpeed;
        if(transform.position == pathPoints[pathPointIndex + 1]){
            rb.velocity = Vector2.zero;
            pathPointIndex++;
        }
    }
}
