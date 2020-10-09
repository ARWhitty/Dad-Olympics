using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    //declarations
    private Transform startPoint;
    public Transform[] Waypoints;
    //private int point = 0;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform point in Waypoints)
        {
            transform.position = Vector3.Lerp(transform.position, point.position, Time.deltaTime);
            
        }
    }
}
