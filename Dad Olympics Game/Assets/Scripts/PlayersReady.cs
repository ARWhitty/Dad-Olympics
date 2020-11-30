using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayersReady : MonoBehaviour
{
    public GameObject gameManager;
    List<GameObject> players;
    // Start is called before the first frame update
    void Start()
    {
        players = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        bool allReady = true;
        other.gameObject.GetComponent<CharacterMovementController>().ReadyPlayer();
        players = gameManager.GetComponent<ManagePlayerHub>().getPlayers();
        foreach(GameObject player in players)
        {
            if (!player.GetComponent<CharacterMovementController>().GetReadyPlayer())
            {
                allReady = false;
            }
        }
        if (allReady)
        {
            gameManager.GetComponent<ManagePlayerHub>().SaveState();
            SceneManager.LoadScene("TheBlock_Scott");
        }

    }
}
