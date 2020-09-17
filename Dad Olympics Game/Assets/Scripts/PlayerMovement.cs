using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class that deals with player movement and player input
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    public CharacterController playerController;

    private float moveSpeed;

    public float moveSpeedGrab;

    public float moveSpeedNormal;

    public float turnTime = 0.1f;

    public float jumpPower;

    public float jumpPowerGrab;

    public float throwForce;

    private GameObject grabbedObject;

    private bool hasGrabbed = false;

    private bool hasPickup = false;

    Vector3 offset;

    public float knockBackTime;

    private float knockBackCounter;

    private RaycastHit hitInfo;

    public Text GrabText;


    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = moveSpeedNormal;
    }

    // Update is called once per frame
    void Update()
    {

        //Handle rotation
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * 4f, 0));

        if (knockBackCounter <= 0)
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
            //apply rotation to the movement so player will always go forward in the direction they are facing
            moving = transform.rotation * moving;
            GetComponent<Rigidbody>().AddForce(moving);

            //Handle moving and rotating the object that has been grabbed
            if (hasGrabbed)
            {
                offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 4f, Vector3.up) * offset;
                grabbedObject.transform.position = transform.position + offset;
                grabbedObject.transform.rotation = transform.rotation;
                grabbedObject.transform.rotation *= Quaternion.Euler(0,90,0);
                //grabbedObject.transform.LookAt(transform.position);

                //If player released "e" then let go
                if (Input.GetKeyUp("e"))
                {
                    DropGrabbedItem();
                }


            }else if (hasPickup)
            {
                offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 4f, Vector3.up) * offset;
                grabbedObject.transform.position = transform.position + offset;
                grabbedObject.transform.rotation = transform.rotation;
                if (Input.GetKeyUp("q"))
                {

                    performThrow();
                }
            }

            Vector3 forward = transform.forward;
            forward.y = -1;
            if (Physics.Raycast(transform.position, forward, out hitInfo, 1))
            {
                if (hitInfo.collider.gameObject.tag == "Grabbable")
                {
                    if (hasGrabbed)
                    {
                        GrabText.text = "";
                    }
                    else
                    {
                        GrabText.text = "Press e and hold to grab this object";
                    }

                    if (Input.GetKeyDown("e") && !hasGrabbed && !hasPickup)
                    {

                        grabbedObject = hitInfo.collider.gameObject;
                        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                        grabbedObject.GetComponent<Rigidbody>().useGravity = false;
                        offset = grabbedObject.transform.position - transform.position + (transform.rotation * new Vector3(0, 0, 0.5f));
                        moveSpeed = moveSpeedGrab;
                        hasGrabbed = true;
                    }
                }
                else if (hitInfo.collider.gameObject.tag == "Pickup")
                {
                    if (hasGrabbed)
                    {
                        GrabText.text = "";
                    }
                    else
                    {
                        GrabText.text = "Press q and hold to throw this object";
                    }
                    if (Input.GetKeyDown("q") && !hasGrabbed && !hasPickup)
                    {
                        grabbedObject = hitInfo.collider.gameObject;
                        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                        grabbedObject.GetComponent<Rigidbody>().useGravity = false;
                        offset = grabbedObject.transform.position - transform.position + (transform.rotation * new Vector3(0, 1f, 0.5f));
                        moveSpeed = 0f;
                        hasPickup = true;
                    }

                }

            }
            else
            {
                GrabText.text = "";
            }

        }
        else
        {
            knockBackCounter -= Time.deltaTime;
        }
    }


    //Commenting out the collision code as the RayCast way to detect pickups seems more responsive
    /*
    void OnCollisionStay(Collision obj)
    {
        //Check if the player collided with a grabbable object
        if (obj.collider.tag.Equals("Grabbable"))
        {
            //If the player presses "e" then grab that object
            if (Input.GetKeyDown("e") && !hasGrabbed && !hasPickup)
            {
                grabbedObject = obj.gameObject;
                offset = grabbedObject.transform.position - transform.position + (transform.rotation * new Vector3(0,0,0.5f));
                moveSpeed = moveSpeedGrab;
                hasGrabbed = true;
            }
        }else if (obj.collider.tag.Equals("Pickup"))
        {
            if (Input.GetKeyDown("q") && !hasGrabbed && !hasPickup)
            {
                Debug.Log("Grabbed pickup obj");
                grabbedObject = obj.gameObject;
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                grabbedObject.GetComponent<Rigidbody>().useGravity = false;
                offset = grabbedObject.transform.position - transform.position + (transform.rotation * new Vector3(0, 1f, 0.5f));
                moveSpeed = 0f;
                hasPickup = true;
            }
        }
    }*/

    //Simple method that checks if the player is grounded using raycast
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.5f);
    }

    public void KnockBack(Vector3 direction)
    {
        Debug.Log("KBCalled");
        knockBackCounter = knockBackTime;
        DropGrabbedItem();
        GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
    }

    private void DropGrabbedItem()
    {
        grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
        grabbedObject.GetComponent<Rigidbody>().useGravity = true;
        grabbedObject = null;
        hasGrabbed = false;
        moveSpeed = moveSpeedNormal;
    }

    private void performThrow()
    {
        Vector3 forward = transform.forward;
        grabbedObject.transform.forward = forward;
        forward.y = 1.5f;
        Vector3 throwingForce = forward * throwForce; //transform.rotation.normalized * new Vector3(0, throwForce*2000, throwForce*300);
        Debug.Log("Threw with Vector force: " + throwingForce);
        grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
        grabbedObject.GetComponent<Rigidbody>().useGravity = true;
        grabbedObject.GetComponent<Rigidbody>().AddForce(throwingForce);
        hasPickup = false;
        moveSpeed = moveSpeedNormal;
        grabbedObject = null;
    }
}