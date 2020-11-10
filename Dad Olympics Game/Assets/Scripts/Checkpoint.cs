using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    public int checkPointNum;

    public Text checkPointText;

    public float swapTime;
    private float timeCount;
    private bool display;
    private int playerID;
    // Start is called before the first frame update
    void Start()
    {
        display = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (display)
        {
            Debug.Log("Text active");
            timeCount += Time.deltaTime;
            checkPointText.text = "Player " + playerID + " has passed checkpoint " + checkPointNum + "!";
            if(timeCount >= swapTime)
            {
                display = false;
                timeCount = 0;
                checkPointText.text = "";
            }
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.Equals("Player"))
        {
            Debug.Log("Collided with player");
            if((collider.gameObject.GetComponent<CharacterMovementController>().getCheckpointCount() == checkPointNum - 1) && collider.gameObject.GetComponent<CharacterMovementController>().GetHasGrabbed())
            {
                Debug.Log("Time to display");
                collider.gameObject.GetComponent<CharacterMovementController>().SetCheckpointCount(checkPointNum);
                display = true;
                playerID = collider.gameObject.GetComponent<CharacterMovementController>().getPlayerID() + 1;
            }

        }
    }
}
