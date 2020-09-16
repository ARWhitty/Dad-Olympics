<<<<<<< Updated upstream
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public short kBForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Vector3 direction = other.transform.position - transform.position;
            direction = direction.normalized + new Vector3(0,1,-3);
            other.gameObject.GetComponent<PlayerMovement>().KnockBack(direction * kBForce);
        }
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public short kBForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Vector3 direction = other.transform.position - transform.position;
            direction = direction.normalized + new Vector3(0,1,-3);
            other.gameObject.GetComponent<PlayerMovement>().KnockBack(direction * kBForce);
        }
    }
}
>>>>>>> Stashed changes
