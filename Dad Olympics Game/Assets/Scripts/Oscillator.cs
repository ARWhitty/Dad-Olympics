using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    public Vector3 pointANucleus, pointBNucleus;
    public short speed;
    public float pointRadius = 5;
    public bool toPointA;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var myBody = gameObject.GetComponent<Rigidbody>();
        if (toPointA)
        {
            if (Vector3.Distance(myBody.position, pointANucleus) <= pointRadius)
            {
                toPointA = false;
            }
            myBody.velocity = (Vector3.MoveTowards(transform.position, pointANucleus, 500) - transform.position).normalized * speed;

        }
        else
        {
            if (Vector3.Distance(myBody.position, pointBNucleus) <= pointRadius)
            {
                toPointA = true;
            }
            myBody.velocity = (Vector3.MoveTowards(transform.position, pointBNucleus, 500) - transform.position).normalized * speed;

        }
    }
}