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

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
        startPos = player.transform.position;
        gameRunning = true;
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




            if (Input.GetKeyDown("r"))
            {
                ResetGame();
            }
        }

    }

    private void ResetGame()
    {
        currentTime = startTime;
        player.transform.position = startPos;
    }

    void OnCollisionEnter(Collision obj)
    {
        Debug.Log("Trigger end");
        gameRunning = false;

    }
}



