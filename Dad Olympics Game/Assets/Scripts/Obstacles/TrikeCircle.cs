using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrikeCircle : MonoBehaviour
{

    //Place a waypoint marker here
    public GameObject center;
    public short speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(center.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}
