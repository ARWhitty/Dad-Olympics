using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class CubeController : MonoBehaviour
{
    Vector2 movement;
    float moveSpeed = 10f;



    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        transform.Translate(move * moveSpeed * Time.deltaTime);
    }

    public void OnMove(CallbackContext context)
    {

        movement = context.ReadValue<Vector2>();
    }
}
