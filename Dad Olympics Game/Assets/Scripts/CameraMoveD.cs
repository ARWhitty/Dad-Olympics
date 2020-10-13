using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera Move for Prototype (Depricated)
/// </summary>
public class CameraMoveD : MonoBehaviour
{

    public float cameraMoveSpeed;

    public float mouseSensitivity = 100.0f;
    public float clampAngle = 80.0f;

    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * 2f;
        float mouseY = -Input.GetAxis("Mouse Y") * 2f;

        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
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
        Vector3 moving = movementVector * cameraMoveSpeed;
        //apply rotation to the movement so player will always go forward in the direction they are facing
        moving = transform.rotation * moving;
        GetComponent<Rigidbody>().AddForce(moving);
    }
}
