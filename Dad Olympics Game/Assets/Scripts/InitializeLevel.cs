using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    public Transform[] playerSpawns;
    public GameObject playerPrefab;


    // Start is called before the first frame update
    void Start()
    {
        var players = ManagePlayerHub.Instance.getPlayers().ToArray();
        for(int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = playerSpawns[i].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
