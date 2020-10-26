using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;



public class CameraMove : MonoBehaviour
{
    public float rotationSpeedLateral, rotationSpeedVertical;
    public Transform target, player;
    float mouseX, mouseY;
    public float minY, maxY;
    private Vector2 direction;

    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        direction = new Vector2();
    }

    private void LateUpdate()
    {
        Control();
        //For editor only!
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }*/
    }

    void Control()
    {

        mouseX += direction.x * rotationSpeedLateral;
        mouseY -= direction.y * rotationSpeedVertical;
        mouseY = Mathf.Clamp(mouseY, minY, maxY);

        transform.LookAt(target);

        target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
    }

    /*public void OnMove(CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }*/

    public void SetDirectionVector(Vector2 dir)
    {
        direction = dir;
    }
}
