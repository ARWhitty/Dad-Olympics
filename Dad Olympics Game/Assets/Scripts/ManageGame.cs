using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManageGame : MonoBehaviour
{

    private List<PlayerConfiguration> configs;

    public GameObject[] checkpoints;

    public Text FinishText;

    public float endTimer;

    public float swapTime;
    private float timeCount;
    private bool display;
    private int playerID;
    private float timer;
    private bool playerFinish = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (display)
        {
            Debug.Log("Text active");
            timeCount += Time.deltaTime;
            FinishText.text = "Player " + playerID + " has finished the race!";
            playerFinish = true;
            if (timeCount >= swapTime)
            {
                display = false;
                timeCount = 0;
            }
        }
        else if (!display)
        {
            FinishText.text = "";
        }

        if (playerFinish)
        {
            timer += Time.deltaTime;
            if(timer >= endTimer)
            {
                SceneManager.LoadScene("VictoryStands");
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collided with end");
        if (collider.tag.Equals("Player"))
        {
            Debug.Log("Player checkpoint: " + collider.gameObject.GetComponent<CharacterMovementController>().getCheckpointCount());
            if(collider.gameObject.GetComponent<CharacterMovementController>().getCheckpointCount() == checkpoints.Length && collider.gameObject.GetComponent<CharacterMovementController>().GetHasGrabbed())
            {
                
                Debug.Log("Finished!");
                display = true;
                playerID = collider.gameObject.GetComponent<CharacterMovementController>().getPlayerID() + 1;
            }
        }
    }

    public void updatePlayerConfigsList(List<PlayerConfiguration> configs)
    {
        foreach(PlayerConfiguration config in configs)
        {
            if (!this.configs.Contains(config))
            {
                this.configs.Add(config);
            }
        }
    }
}
