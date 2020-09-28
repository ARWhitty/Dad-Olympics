using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageGame : MonoBehaviour
{
    public bool isTimerCountDown;

    public float startTime;

    public float currentTime;

    public GameObject player;

    private Vector3 startPos;

    public Text gameTimerText;

    private bool gameRunning;

    public GameObject stroller;

    private Vector3 strollerPos;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
        startPos = player.transform.position;
        gameRunning = true;
        strollerPos = stroller.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameRunning)
        {
            if (currentTime <= 0)
            {
                ResetGame();
            }
            if (isTimerCountDown)
            {
                currentTime -= Time.deltaTime;
                gameTimerText.text = "Time Remaining: " + currentTime;
            }
            else
            {
                currentTime += Time.deltaTime;
                gameTimerText.text = "Current Time: " + currentTime;
            }




        }
        if (Input.GetKeyDown("r"))
        {
            ResetGame();
        }

    }

    private void ResetGame()
    {
        gameRunning = true;
        currentTime = startTime;
        player.transform.position = startPos;
        stroller.transform.position = strollerPos;
    }

    void OnTriggerEnter(Collider obj)
    {
        if (player.GetComponent<PlayerMovement>().GetHasGrabbed())
        {
            Debug.Log("Trigger end");
            gameRunning = false;
        }


    }
}



