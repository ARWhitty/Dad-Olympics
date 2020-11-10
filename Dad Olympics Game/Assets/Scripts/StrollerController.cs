using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrollerController : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void KnockBack(Vector3 direction, bool dropStroller)
    {
        Debug.Log("KBCalled");
        if(dropStroller)
            player.GetComponent<PlayerMovement>().DropGrabbedItem();
        GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
    }
}
