﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterMovementController : MonoBehaviour
{
    private CharacterController controller;
    public Transform camPos;

    private float moveSpeed;
    public float gravity;
    public float jumpHeight;
    Vector3 velocity;

    float turnSmoothVelocity;

    public float moveSpeedGrab;

    public float moveSpeedNormal;

    public float turnTime;

    public float jumpHeightGrab;

    public float throwForce;

    private GameObject grabbedObject;

    private bool hasGrabbed = false;

    public Text GrabText;

    public AudioSource grabSound;

    public AudioSource throwSound;

    public float knockBackTime;

    private float knockBackCounter;


    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = moveSpeedNormal;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKeyDown("space") && IsGrounded())
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

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 dirVector = new Vector3(horizontal, 0f, vertical).normalized;

        if (dirVector.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dirVector.x, dirVector.z) * Mathf.Rad2Deg + camPos.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }



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
                if (Input.GetKeyDown("e"))
                {
                    grabSound.Play();
                    DropGrabbedItem();
                }
                else if (Input.GetKeyDown("q"))
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
        }
    }

    void OnCollisionEnter(Collision obj)
    {
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
        Debug.Log("KBCalled");
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