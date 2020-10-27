using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public Vector3 directionVector;
    public short kBForce;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Vector3 direction = other.transform.position - transform.position;
            direction = direction.normalized + directionVector;
            Debug.Log("direction: " + direction.ToString() + " Force " + kBForce);
            other.gameObject.GetComponent<CharacterMovementController>().KnockBack(direction * kBForce);
        } else if (other.tag == "Grabbable" && kBForce >= 50)
        {
            Vector3 direction = other.transform.position - transform.position;
            direction = direction.normalized;
            other.gameObject.GetComponent<StrollerController>().KnockBack(direction * (kBForce));
        }
    }
}