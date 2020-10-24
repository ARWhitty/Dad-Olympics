using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to define the behavior for an object that follows another object
/// </summary>
public class SimpleFollowOld : MonoBehaviour
{

    public GameObject Player;

    public bool UseOffset = true;

    public float MoveSpeed = 0.05f;

    private Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        if (UseOffset)
        {
            offset = transform.position - Player.transform.position;
        }
    }

    // Call FixedUpdate so it stays synced with the physics engine
    void FixedUpdate()
    {

        transform.position = Vector3.Lerp(transform.position, Player.transform.position + offset, MoveSpeed);

    }


    void LateUpdate()
    {
        //Make sure the object faces towards the character and pivots around depending on the movements of the object being followed
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 4f, Vector3.up) * offset;
        transform.LookAt(Player.transform.position);
    }
}