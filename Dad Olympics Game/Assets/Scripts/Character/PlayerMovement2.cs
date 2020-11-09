using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Player movement version 2
/// </summary>
public class PlayerMovement2 : MonoBehaviour
{
    private float moveSpeed;

    public float moveSpeedGrab;

    public float moveSpeedNormal;

    public float turnTime;

    public float jumpPower;

    public float jumpPowerGrab;

    public float throwForce;

    private GameObject grabbedObject;

    private bool hasGrabbed = false;

    Vector3 offset;

    public float knockBackTime;

    private float knockBackCounter;

    private RaycastHit hitInfo;

    public Text GrabText;

    public AudioSource grabSound;

    public AudioSource throwSound;

    private bool isJumping;

    private float maxSpeed;

    public Transform target;
    public float correctionRotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = moveSpeedNormal;
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space") && IsGrounded())
        {
            isJumping = true;

        }

        if (knockBackCounter <= 0)
        {


            //Handle moving and rotating the object that has been grabbed
            if (hasGrabbed)
            {
                offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 4f, Vector3.up) * offset;
                grabbedObject.transform.position = transform.position + transform.forward * 1.5f;
                grabbedObject.transform.rotation = transform.rotation;
                grabbedObject.transform.rotation *= Quaternion.Euler(0, 90, 0);
                //grabbedObject.transform.LookAt(transform.position);

                //If player released "e" then let go
                if (Input.GetKeyDown("e"))
                {
                    grabSound.Play();
                    DropGrabbedItem();
                }
                else if (Input.GetKeyDown("q"))
                {
                    //throwSound.Play();
                    performThrow();
                }


            }
            else
            {
                HandleGrabObject();
            }

            /*
            Vector3 forward = transform.forward;
            forward.y = -1;
            if (Physics.SphereCast(transform.position, 1f, forward, out hitInfo, 1))
            {
                if (hitInfo.collider.gameObject.tag == "Grabbable")
                {
                    if (hasGrabbed)
                    {
                        GrabText.text = "";
                    }
                    else
                    {
                        GrabText.text = "Press e to grab this object";
                    }

                    if (Input.GetKeyDown("e") && !hasGrabbed)
                    {

                        grabbedObject = hitInfo.collider.gameObject;
                        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                        grabbedObject.GetComponent<Rigidbody>().useGravity = false;
                        offset = grabbedObject.transform.position - transform.position + (transform.rotation * new Vector3(0, 0, 0.5f));
                        moveSpeed = moveSpeedGrab;
                        hasGrabbed = true;
                    }
                }
                

            }
            else
            {
                if(GrabText)
                    GrabText.text = "";
            }*/

        }
        else
        {
            knockBackCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown("o"))
        {
            SceneManager.LoadScene("Prototype_StrollerRace");

        }
        else if (Input.GetKeyDown("p"))
        {
            SceneManager.LoadScene("Prototype-Sandbox");
        }
        else if (Input.GetKeyDown("i"))
        {
            SceneManager.LoadScene("ScottScene2");
        }
    }

    void FixedUpdate()
    {
        Vector3 movementVector = Vector3.zero;

        //Handle z and x movements
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.z = Input.GetAxis("Vertical");

        movementVector = movementVector.normalized;

        //Handle jump mechanic
        if (isJumping)
        {
            if (hasGrabbed)
            {
                movementVector.y += jumpPowerGrab;
            }
            else
            {
                movementVector.y += jumpPower;
            }
            isJumping = false;

        }
        //vector that takes direction and speed
        Vector3 moving = movementVector * moveSpeed;
        //apply rotation to the movement so player will always go forward in the direction the camera is facing
        moving = target.rotation * moving;
        GetComponent<Rigidbody>().AddForce(moving);

        //This adjusts the player's rotation if they decide to move in a direction the character is not facing
        if(!Input.GetAxis("Vertical").Equals(0) && transform.rotation.y != target.rotation.y)
        {
            //This check is here to prevent the character from wabbling
            //if (Mathf.Abs(target.rotation.eulerAngles.y - transform.rotation.eulerAngles.y) > (correctionRotationSpeed/50))
            //{
                float yRotation = Mathf.LerpAngle(0, target.rotation.eulerAngles.y - transform.rotation.eulerAngles.y, 1) * Time.deltaTime * correctionRotationSpeed;
                //Debug.Log("Transform: " + transform.rotation.eulerAngles.y + ", Target: " + target.rotation.eulerAngles.y + ", MoveTowards: " + yRotation);
                transform.Rotate(0, yRotation, 0);
            //}
        }
        if (!IsGrounded())
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, -200f, 0));
        }
        else
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, -130f, 0));
        }
    }

    void OnCollisionEnter(Collision obj)
    {
        Debug.Log("Collided");
        if (obj.collider.tag.Equals("WarpSandbox"))
        {
            Debug.Log("Last Scene");
            SceneManager.LoadScene("ScottScene2");
        }
        if (obj.gameObject.tag == "Ground" && obj.impulse.magnitude > 150) //If you hit the ground with more than 300 force.
        {
            float yRot = GetComponent<Rigidbody>().rotation.y % 360;
            StartCoroutine(KnockDown(new Vector3(-Mathf.Cos(yRot), -1, -Mathf.Sin(yRot))));
        }
    }

    //Simple method that checks if the player is grounded using raycast
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1f);
    }

    public IEnumerator KnockDown(Vector3 fallDirection)
    {
        Rigidbody body = GetComponent<Rigidbody>();
        body.freezeRotation = false;
        body.AddForce(fallDirection * 200, ForceMode.Impulse);
        knockBackCounter = 60;
        yield return new WaitForSeconds(1);
        knockBackCounter = 30;
        body.position += new Vector3(0, 3, 0);
        body.rotation = Quaternion.identity;
        body.freezeRotation = true;

    }

    public void KnockBack(Vector3 direction)
    {
        //Debug.Log("KBCalled");
        knockBackCounter = knockBackTime;
        DropGrabbedItem();
        GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
    }

    public void DropGrabbedItem()
    {
        if (grabbedObject)
        {
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject.GetComponent<Rigidbody>().useGravity = true;
            grabbedObject = null;
            hasGrabbed = false;
            moveSpeed = moveSpeedNormal;
        }
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
        hasGrabbed = false;
        moveSpeed = moveSpeedNormal;
        grabbedObject = null;
    }

    private void HandleGrabObject()
    {
        bool foundGrabbable = false;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1);
        foreach (Collider collider in hitColliders)
        {
            if (collider.tag == "Grabbable")
            {
                foundGrabbable = true;
                if (hasGrabbed)
                {
                    GrabText.text = "";
                }
                else
                {
                    GrabText.text = "Press e to grab this object";
                }

                if (Input.GetKeyDown("e") && !hasGrabbed)
                {
                    grabSound.Play();
                    grabbedObject = collider.gameObject;
                    grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                    grabbedObject.GetComponent<Rigidbody>().useGravity = false;
                    moveSpeed = moveSpeedGrab;
                    hasGrabbed = true;
                }
            }
        }
        if (!foundGrabbable)
        {
            GrabText.text = "";
        }
    }

    public bool GetHasGrabbed()
    {
        return hasGrabbed;
    }
}
