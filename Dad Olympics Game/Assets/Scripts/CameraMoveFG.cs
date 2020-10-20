using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveFG : MonoBehaviour
{
    public float rotationSpeedLateral, rotationSpeedVertical;
    public Transform target, player;
    float mouseX, mouseY;
    public float minY, maxY, playerRotation;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        playerRotation += horizontalInput;
        Control(playerRotation);
        //For editor only!
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void Control(float horizontalInput)
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeedLateral;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeedVertical;
        mouseY = Mathf.Clamp(mouseY, minY, maxY);

        transform.LookAt(target);

        target.rotation = Quaternion.Euler(mouseY, mouseX + horizontalInput, 0);
    }
}
