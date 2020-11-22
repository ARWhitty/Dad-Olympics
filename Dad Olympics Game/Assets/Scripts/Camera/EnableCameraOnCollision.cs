using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCameraOnCollision : MonoBehaviour
{
    public CinemachineVirtualCamera myCamera;

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("OnTriggerEnter Called.");
        if (collision.tag == "Player")
        {
            if (myCamera)
            {
                myCamera.enabled = true;
                myCamera.LookAt = collision.transform;
            }
            else
            {
                Debug.LogError("You did not assign a virtual camera to this script.");
            }
        }
    }
    

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            myCamera.enabled = false;
        }
    }
}
