using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * 4f, 0));
        Vector3 movementVector = Vector3.zero;

        //Handle z and x movements
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.z = Input.GetAxis("Vertical");
        movementVector = movementVector.normalized;
        if (Input.GetKeyDown("e"))
        {
            movementVector.y += 60;
        }else if (Input.GetKeyDown("q"))
        {
            movementVector.y -= 60;
        }
        //vector that takes direction and speed
        Vector3 moving = movementVector * 5;
        //apply rotation to the movement so player will always go forward in the direction they are facing
        moving = transform.rotation * moving;
        GetComponent<Rigidbody>().AddForce(moving);
    }
}
