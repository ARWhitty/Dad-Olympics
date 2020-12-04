using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    public int checkPointNum;

    public Text checkPointText;

    public float swapTime;

    public string soundName;

    public AudioSource mySource;
    private float timeCount;
    private bool display;
    private int playerID;
    // Start is called before the first frame update
    void Start()
    {
        display = false;
        //Debug.Log("Searching for : " + soundName);
        GameObject sfx = GameObject.Find("SFX");
        Transform trans = sfx.transform;
        Transform target = trans.Find(soundName);
        mySource = target.gameObject.GetComponent<AudioSource>();
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
            CharacterMovementController player = collider.gameObject.GetComponent<CharacterMovementController>();
            GameObject stroller = collider.gameObject.GetComponent<CharacterMovementController>().GetGrabbedObject();
            if ((player.getCheckpointCount() == checkPointNum - 1) && stroller != null)
            {
                if((stroller.GetComponent<StrollerController>().StrollerID == player.getPlayerID()) || player.isAI)
                {
                    player.SetCheckpointCount(checkPointNum);
                    display = true;
                    playerID = player.getPlayerID() + 1;
                    if(player.isAI == false)
                    {
                        mySource.Play();
                    }
                }

            }

        }
    }
}
