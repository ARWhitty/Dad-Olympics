using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementKieran : MonoBehaviour
{
    public float rotationSpeedLateral, rotationSpeedVertical;

    public GameObject player;

    public bool UseOffset = true;

    public float MoveSpeed;

    private Vector3 offset;

    float mouseX, mouseY;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (UseOffset)
        {
            offset = transform.position - player.transform.position;
        }
    }

    // Call FixedUpdate so it stays synced with the physics engine
    void FixedUpdate()
    {


    }

    private void LateUpdate()
    {
        Control();
        //For editor only!
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void Control()
    {

            //transform.position = Vector3.Lerp(transform.position, player.transform.TransformPoint(offset), 0.005f);
            //transform.LookAt(player.transform.position);


            mouseX += Input.GetAxis("Mouse X") * rotationSpeedLateral;
            mouseY -= Input.GetAxis("Mouse Y") * rotationSpeedVertical;
            mouseY = Mathf.Clamp(mouseY, 10, 70);

            transform.LookAt(player.transform.position);
            transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        //transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, MoveSpeed * Time.deltaTime);
        


    }
}
