using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class CharacterMovementController : MonoBehaviour
{
    private CharacterController controller;
    public Transform camPos;
    public Animator animator;

    public GameObject playerCamera;

    private float moveSpeed;
    public float gravity;
    public float jumpHeight;
    Vector3 velocity;
    Vector3 externalVelocity;

    private Vector2 direction;

    float turnSmoothVelocity;

    public float moveSpeedGrab;

    public float moveSpeedNormal;

    public float turnTime;

    public float jumpHeightGrab;

    public float throwForce;

    private GameObject grabbedObject;

    private bool hasGrabbed = false;

    //public Text GrabText;

    public AudioSource grabSound;

    public AudioSource throwSound;

    public float knockBackTime;

    public float groundFriction;

    private float knockBackCounter;

    private bool isJumping;

    private bool pickupPressed;

    private bool throwPressed;

    private bool airborn; 

    private float pickupCooldown;

    private Vector3 externalForce;


    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = moveSpeedNormal;
        direction = new Vector2();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (IsGrounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        }*/



        Move();

        Jump();

        CollisionDetection();

        /*if (dirVector.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dirVector.x, dirVector.z) * Mathf.Rad2Deg + camPos.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }*/



       if (knockBackCounter <= 0)
        {


            //Handle moving and rotating the object that has been grabbed
            if (hasGrabbed)
            {
                grabbedObject.transform.position = transform.position + transform.forward * 1.5f;
                grabbedObject.transform.rotation = transform.rotation;
                grabbedObject.transform.rotation *= Quaternion.Euler(0, 90, 0);
                //grabbedObject.transform.LookAt(transform.position);

                //If player released "e" then let go
                if (pickupPressed)
                {
                    grabSound.Play();
                    DropGrabbedItem();
                    pickupPressed = false;
                }
                else if (throwPressed)
                {
                    performThrow();
                }


            }
            else
            {
                HandleGrabObject();
            }

        }
        else
        {
            knockBackCounter -= Time.deltaTime;
            //controller.Move(externalForce);
            if (knockBackCounter <= 0)
            {
                externalForce = Vector3.zero;
            }
        }
       if(pickupCooldown > 0)
        {
            pickupCooldown -= 1;
        }

    }

    private void CollisionDetection()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down); 
        if (airborn)
        {
            if (Physics.Raycast(ray, out hit, 1.0f))
            {
                if (hit.transform.tag == "Ground")
                {
                    velocity.x = 0;
                    velocity.z = 0;
                    airborn = false;
                }
            }
        }
        else
        {
            if (Physics.Raycast(ray, out hit, 1.0f) == false || hit.transform.tag != "Ground")
            {
                airborn = true;
            }
        }
    }

    private void Jump()
    {
        if (IsGrounded() && isJumping)
        {
            if (hasGrabbed)
            {
                velocity.y = Mathf.Sqrt(jumpHeightGrab * -2 * gravity);
            }
            else
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Move()
    {
        Vector3 dirVector = new Vector3(direction.x, 0f, direction.y).normalized;
        //controller.Move(dirVector * moveSpeed * Time.deltaTime);

        if (dirVector.magnitude >= 0.1f)
        {
            animator.SetBool("isRunning", true);
            float targetAngle = Mathf.Atan2(dirVector.x, dirVector.z) * Mathf.Rad2Deg + camPos.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move((moveDir.normalized * moveSpeed) * Time.deltaTime);
            //velocity += (moveDir.normalized * moveSpeed) * Time.deltaTime;
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    public void OnMove(CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    public void OnCameraMove(CallbackContext context)
    {
        playerCamera.GetComponent<CameraMoveFG>().SetDirectionVector(context.ReadValue<Vector2>());
    }

    public void OnJump(CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }

    }

    public void OnPickup(CallbackContext context)
    {
        if (context.performed)
        { 
            pickupPressed = true;

        }
        else
        {
            pickupPressed = false;
        }
    }

    public void OnThrow(CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            throwPressed = true;
        }
        else
        {
            throwPressed = false;
        }
    }

    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "Ground")
         {
            velocity.x = 0;
            velocity.z = 0;
        }
    }


    //Simple method that checks if the player is grounded using raycast
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.5f);
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
        Debug.Log("KBCalled with: " + direction.ToString());
        knockBackCounter = knockBackTime;
        DropGrabbedItem();
        velocity = direction;
        
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
                    //GrabText.text = "";
                }
                else
                {
                    //GrabText.text = "Press e to grab this object";
                }

                if (pickupPressed && !hasGrabbed)
                {
                    grabSound.Play();
                    grabbedObject = collider.gameObject;
                    grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                    grabbedObject.GetComponent<Rigidbody>().useGravity = false;
                    moveSpeed = moveSpeedGrab;
                    hasGrabbed = true;
                    pickupPressed = false;
                }
            }
        }
        if (!foundGrabbable)
        {
            //GrabText.text = "";
        }
    }

    public bool GetHasGrabbed()
    {
        return hasGrabbed;
    }
}
