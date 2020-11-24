using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    public GameObject path;

    private List<Transform> pathNodes;

    private int index;

    public GameObject stroller;

    private bool start;

    public int ID;

    // Start is called before the first frame update
    void Start()
    {
        pathNodes = path.GetComponent<Path_AI>().getPath();
        GetComponent<CharacterMovementController>().SetGrabbedObject(stroller);
        GetComponent<CharacterMovementController>().SetPlayerID(ID);
        index = 0;
        start = false;
    }

    // Update is called once per frame
    void Update()
    {
        SelectCurrentNode();


    }

    private void SelectCurrentNode()
    {
        if (!GetComponent<CharacterMovementController>().GetHasGrabbed())
        {
            transform.LookAt(new Vector3(stroller.transform.position.x, transform.position.y, stroller.transform.position.z));
            GetComponent<CharacterMovementController>().Move(transform.forward);
            if (Math.Abs((stroller.transform.position - transform.position).magnitude) < 3)
            {
                GetComponent<CharacterMovementController>().SetGrabbedObject(stroller);
            }
        }
        else if (index+1 <= pathNodes.Count && start)
        {
            if (Math.Abs((pathNodes[index].position - transform.position).magnitude) < 1)
            {
                index++;
            }
            else
            {
                transform.LookAt(new Vector3(pathNodes[index].position.x, transform.position.y, pathNodes[index].position.z));
                GetComponent<CharacterMovementController>().Move(transform.forward);
                if (pathNodes[index].tag.Equals("JumpTime"))
                {
                    Debug.Log("Time to jump");
                    GetComponent<CharacterMovementController>().isJumping = true;
                }
                else
                {
                    GetComponent<CharacterMovementController>().isJumping = false;
                }
            }
        }
        else
        {
            GetComponent<CharacterMovementController>().Move(new Vector3(0,0,0));
        }
    }

    public void StartAI()
    {
        start = true;
    }
}
