using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
    

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Transform path;

    protected List<Vector3> pathPoints = new List<Vector3>();
    protected int pathPointIndex = 0;

    void Start()
    {
        foreach(Transform point in path){
            pathPoints.Add(point.position);
        }
        transform.position = pathPoints[0];
    }

    void Update()
    {
        if((pathPointIndex + 1) >= pathPoints.Count) Destroy(gameObject);
        transform.position = Vector3.MoveTowards(transform.position, pathPoints[pathPointIndex + 1], moveSpeed * Time.deltaTime);
        if(transform.position == pathPoints[pathPointIndex + 1]) pathPointIndex++;
    }
}
