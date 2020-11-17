﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
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

    private float knockBackCounter;

    public bool isJumping;

    private bool pickupPressed;

    private bool throwPressed;

    public bool airborn;

    public bool beingKnockedBack;

    public bool movementEnabled = true;

    public float timeUntilMoveEnabled = 0;

    private float pickupCooldown;

    private int playerID;

    private int completedCheckpoints;


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
        if(movementEnabled)
            Move();
        else
        {
            timeUntilMoveEnabled -= Time.deltaTime;
            if(timeUntilMoveEnabled < 0)
            {
                timeUntilMoveEnabled = 0;
                movementEnabled = true;
            }
        }

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
               grabbedObject.transform.position = transform.position + (transform.forward * 1.3f) + (transform.up * 0.7f);
               grabbedObject.transform.rotation = transform.rotation;
               grabbedObject.transform.rotation *= Quaternion.Euler(0, 90, 0);
                //grabbedObject.transform.LookAt(transform.position);

                //If player released "e" then let go
                if (pickupPressed)
                {
                    grabSound.Play();
                    DropGrabbedItem();
                    pickupPressed = false;
                    animator.SetBool("isHoldingSomething", false);
                }
                else if (throwPressed)
                {
                    StartCoroutine(performThrow());
                    animator.SetBool("isHoldingSomething", false);
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
            if (hasGrabbed)
            {
                grabbedObject.transform.position = transform.position + (transform.forward * 1.3f) + (transform.up * 0.7f); // Added transform.up because stroller is in the ground with new dad model.
                grabbedObject.transform.rotation = transform.rotation;
                grabbedObject.transform.rotation *= Quaternion.Euler(0, 90, 0);
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
            if (Physics.Raycast(ray, out hit, 0.1f) && beingKnockedBack)
            {
                if (hit.transform.tag == "Ground")
                {
                    animator.ResetTrigger("Jump");
                    animator.SetTrigger("Land");
                    animator.SetTrigger("FallDown");
                    beingKnockedBack = false;
                    velocity.x = 0;
                    velocity.z = 0;
                    airborn = false;
                    movementEnabled = false;
                    timeUntilMoveEnabled = 1.5f;
                }
            } else if(Physics.Raycast(ray, out hit, 0.1f))
            {

                animator.ResetTrigger("Jump");
                animator.SetTrigger("Land");
                velocity.x = 0;
                velocity.z = 0;
                airborn = false;
            }
        }
        else
        {
            if (Physics.Raycast(ray, out hit, 0.1f) == false || hit.transform.tag != "Ground")
            {
                airborn = true;
            } else if (Physics.Raycast(ray, out hit, 0.1f) && beingKnockedBack)
            {
                animator.ResetTrigger("Jump");
                animator.ResetTrigger("Land");
                animator.SetTrigger("FallDown");
                beingKnockedBack = false;
                movementEnabled = false;
                timeUntilMoveEnabled = 1.5f;
            }
        }
    }

    private void Jump()
    {
        if (airborn == false && isJumping)
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
        }
        else
        {
            animator.SetBool("isRunning", false);
            velocity.x = 0;
            velocity.z = 0;
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
            animator.ResetTrigger("Land");
            animator.SetTrigger("Jump");
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

    public void KnockBack(Vector3 direction, bool drop)
    {
        //Debug.Log("KBCalled with: " + direction.ToString());
        knockBackCounter = knockBackTime;
        if (drop)
        {
            DropGrabbedItem();
            beingKnockedBack = true;
        }
        velocity = direction;
        
    }

    public void DropGrabbedItem()
    {
        if (grabbedObject)
        {
            animator.SetBool("isHoldingSomething", false);
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject.GetComponent<Rigidbody>().useGravity = true;
            grabbedObject = null;
            hasGrabbed = false;
            moveSpeed = moveSpeedNormal;
        }
    }

    private IEnumerator performThrow()
    {
        animator.ResetTrigger("Land");
        animator.SetTrigger("Throw");
        yield return new WaitForSeconds(1.5f);
        Vector3 forward = transform.forward;
        grabbedObject.transform.forward = forward;
        Vector3 throwingForce = forward * throwForce * 1.5f; //transform.rotation.normalized * new Vector3(0, throwForce*2000, throwForce*300);
        Vector3 movementAdjust = forward * direction.magnitude * moveSpeedGrab * 40;
        throwingForce += movementAdjust;
        throwingForce.y = 300f;
        Debug.Log("Threw with Vector force: " + throwingForce);
        grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
        grabbedObject.GetComponent<Rigidbody>().useGravity = true;
        Debug.Log("Velocity: " + direction.magnitude);
        grabbedObject.GetComponent<Rigidbody>().AddForce(throwingForce);
        hasGrabbed = false;
        moveSpeed = moveSpeedNormal;
        grabbedObject = null;
        animator.ResetTrigger("Throw");
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
                    animator.SetBool("isHoldingSomething", true);
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

    public void SetPlayerID(int ID)
    {
        playerID = ID;
    }

    public int getPlayerID()
    {
        return playerID;
    }

    public void SetCheckpointCount(int count)
    {
        completedCheckpoints = count;
    }

    public int getCheckpointCount()
    {
        return completedCheckpoints;
    }

    public void setMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public float getMoveSpeed()
    {
        return moveSpeed;
    }
}
