<<<<<<< Updated upstream
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    public Vector3 lesserXPoint, greaterXPoint;
    public short xMin, xMax;
    public short speed;
    public bool toPointA;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (toPointA) {
            if (gameObject.transform.position.x < lesserXPoint.x)
            {
                toPointA = false;
            }
            if (gameObject.transform.position.x < xMin)
            {
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-gameObject.GetComponent<Rigidbody>().velocity.x, 0, 0);
            }
            else
            {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.MoveTowards(transform.position, lesserXPoint, 500).normalized * speed;
            }
        } else
        {
            if (gameObject.transform.position.x > greaterXPoint.x)
            {
                toPointA = true;
            }
            if (gameObject.transform.position.x > xMax)
            {
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-gameObject.GetComponent<Rigidbody>().velocity.x, 0, 0);
            } else
            {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.MoveTowards(transform.position, greaterXPoint, 500).normalized * speed;
            }
        }
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    public Vector3 lesserXPoint, greaterXPoint;
    public short xMin, xMax;
    public short speed;
    public bool toPointA;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (toPointA) {
            if (gameObject.transform.position.x < lesserXPoint.x)
            {
                toPointA = false;
            }
            if (gameObject.transform.position.x < xMin)
            {
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-gameObject.GetComponent<Rigidbody>().velocity.x, 0, 0);
            }
            else
            {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.MoveTowards(transform.position, lesserXPoint, 500).normalized * speed;
            }
        } else
        {
            if (gameObject.transform.position.x > greaterXPoint.x)
            {
                toPointA = true;
            }
            if (gameObject.transform.position.x > xMax)
            {
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-gameObject.GetComponent<Rigidbody>().velocity.x, 0, 0);
            } else
            {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.MoveTowards(transform.position, greaterXPoint, 500).normalized * speed;
            }
        }
    }
}
>>>>>>> Stashed changes
