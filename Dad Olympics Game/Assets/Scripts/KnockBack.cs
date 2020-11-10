using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public Vector3 directionVector;
    public short kBForce;
    public bool makePlayerDrop;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Vector3 direction = other.transform.position - transform.position;
            direction = direction.normalized + directionVector;
            Debug.Log("direction: " + direction.ToString() + " Force " + kBForce + " MakePlayerDrop " + makePlayerDrop);
            other.gameObject.GetComponent<CharacterMovementController>().KnockBack(direction * kBForce, makePlayerDrop);
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