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

    public string soundName;

    public AudioSource mySource;

    private float timeCount;
    private bool display;
    private int playerID;
    private float timer;
    private bool playerFinish = false;

    private int positions = 1;

    // Start is called before the first frame update
    void Start()
    {
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
                if(collider.gameObject.GetComponent<CharacterMovementController>().GetFinishPosition() == -1)
                {
                    Debug.Log("Finished!");
                    display = true;
                    playerID = collider.gameObject.GetComponent<CharacterMovementController>().getPlayerID() + 1;
                    collider.gameObject.GetComponent<CharacterMovementController>().SetFinishPosition(positions);
                    positions++;
                    if (collider.gameObject.GetComponent<CharacterMovementController>().isAI == false)
                    {
                        mySource.Play();
                    }
                }

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
