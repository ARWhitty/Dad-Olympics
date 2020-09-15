using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class that deals with player movement and player input
/// </summary>
public class PlayerMovement : MonoBehaviour { 

    private float moveSpeed = 8f;

    public float moveSpeedGrab = 3f;

    public float moveSpeedNormal = 8f;

    public float turnTime = 0.1f;

    public float jumpPower = 50f;

    public float jumpPowerGrab = 100f;

    private GameObject grabbedObject;

    private bool hasGrabbed = false;

    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movementVector = Vector3.zero;

        //Handle z and x movements
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.z = Input.GetAxis("Vertical");

        //Handle jump mechanic
        if (Input.GetKeyDown("space") && IsGrounded())
        {
            if (hasGrabbed)
            {
                movementVector.y += jumpPowerGrab;
            }
            else
            {
                movementVector.y += jumpPower;
            }

        }
        //vector that takes direction and speed
        Vector3 moving = movementVector * moveSpeed;
        //Handle rotation
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * 4f, 0));
        //apply rotation to the movement so player will always go forward in the direction they are facing
        moving = transform.rotation * moving;
        GetComponent<Rigidbody>().AddForce(moving);

        //Handle moving and rotating the object that has been grabbed
        if (hasGrabbed)
        {
            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 4f, Vector3.up) * offset;
            grabbedObject.transform.position = transform.position + offset;
            gameObject.transform.rotation = transform.rotation;
            grabbedObject.transform.LookAt(transform.position);
            grabbedObject.transform.rotation *= Quaternion.Euler(0, -90, 0);
            //If player released "e" then let go
            if (Input.GetKeyUp("e"))
            {
                grabbedObject = null;
                hasGrabbed = false;
                moveSpeed = moveSpeedNormal;
            }


        }
    }

    void OnCollisionStay(Collision obj)
    {
        //Check if the player collided with a grabbable object
        if (obj.collider.tag.Equals("Grabbable"))
        {
            //If the player presses "e" then grab that object
            if (Input.GetKeyDown("e"))
            {
                grabbedObject = obj.gameObject;
                offset = grabbedObject.transform.position - transform.position + (transform.rotation * new Vector3(0,0,0.5f));
                moveSpeed = moveSpeedGrab;
                hasGrabbed = true;
            }
        }
    }

    //Simple method that checks if the player is grounded using raycast
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.5f);
    }


}
