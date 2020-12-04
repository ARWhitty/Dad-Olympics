using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public Vector3 directionVector;
    public short kBForce;
    public bool makePlayerDrop;
    public string KBSound;
    public AudioSource mySource;

    private void Start()
    {
        if(!KBSound.Equals(""))
        {
            //Debug.Log("Searching for : " + KBSound);
            GameObject sfx = GameObject.Find("SFX");
            Transform trans = sfx.transform;
            Transform target = trans.Find(KBSound);
            mySource = target.gameObject.GetComponent<AudioSource>();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Vector3 direction = other.transform.position - transform.position;
            direction = direction.normalized + directionVector;
            //Debug.Log("direction: " + direction.ToString() + " Force " + kBForce + " MakePlayerDrop " + makePlayerDrop);
            other.gameObject.GetComponent<CharacterMovementController>().KnockBack(direction * kBForce, makePlayerDrop);
            if(!KBSound.Equals("") && other.gameObject.GetComponent<CharacterMovementController>().isAI == false)
            {
                mySource.Play();
            }
        } else if (other.tag == "Grabbable")
        {
            Vector3 direction = other.transform.position - transform.position;
            direction = direction.normalized + directionVector;
            other.gameObject.GetComponent<StrollerController>().KnockBack(direction * (kBForce), makePlayerDrop);
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Grabbable")
        {
            Vector3 direction = other.transform.position - transform.position;
            direction = direction.normalized + directionVector;
            other.gameObject.GetComponent<StrollerController>().KnockBack(direction * (kBForce), makePlayerDrop);
        }
    }
}