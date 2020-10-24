using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to define the behavior for an object that follows another object
/// </summary>
public class SimpleFollow : MonoBehaviour
{

    public GameObject Player;

    public bool UseOffset = true;

    public float MoveSpeed = 0.05f;

    private Vector3 offset;

    public Camera cameraComp;
    private float startingFOV;

    public float minFOV, maxFOV, zoomRate;

    private float currentFieldOfView;


    // Start is called before the first frame update
    void Start()
    {
        cameraComp = GetComponent<Camera>();
        startingFOV = cameraComp.fieldOfView;
        Cursor.visible = false;
        if (UseOffset)
        {
            offset = transform.position - Player.transform.position;
        }
    }

    // Call FixedUpdate so it stays synced with the physics engine
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
        }

        currentFieldOfView = cameraComp.fieldOfView;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        
        currentFieldOfView += scroll * zoomRate;

        currentFieldOfView = Mathf.Clamp(currentFieldOfView, minFOV, maxFOV);
        cameraComp.fieldOfView = currentFieldOfView;

        transform.position = Vector3.Lerp(transform.position, Player.transform.position + offset, MoveSpeed);

    }


    void LateUpdate()
    {
        //Make sure the object faces towards the character and pivots around depending on the movements of the object being followed
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 4f, Vector3.up) * offset;
        //Does this work with the Y axis as well?
        offset += Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * 4f, Vector3.up) * offset;
        transform.LookAt(Player.transform.position);
    }
}