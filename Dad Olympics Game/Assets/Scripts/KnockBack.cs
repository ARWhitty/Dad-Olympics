using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public Vector3 directionVector;
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
        if (other.tag == "Player")
        {
            Vector3 direction = other.transform.position - transform.position;
            direction = direction.normalized + directionVector;
            other.gameObject.GetComponent<CharacterMovementController>().KnockBack(direction * kBForce);
        } else if (other.tag == "Grabbable" && kBForce >= 50)
        {
            Vector3 direction = other.transform.position - transform.position;
            direction = direction.normalized;
            other.gameObject.GetComponent<StrollerController>().KnockBack(direction * (kBForce));
        }
    }
}