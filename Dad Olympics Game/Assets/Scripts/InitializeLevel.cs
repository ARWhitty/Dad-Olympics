using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    public Transform[] playerSpawns;
    public GameObject playerPrefab;

    private bool spawnedPlayers = false;
    private float waitTime = 1.5f;
    private float currentTime = 0;

    public Material strollerColor1;
    public Material strollerColor2;
    public Material strollerColor3;
    public Material strollerColor4;
    public Material strollerColor5;

    private GameObject[] players;

    public GameObject strollerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        players = ManagePlayerHub.Instance.getPlayers().ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > waitTime && !spawnedPlayers)
        {
            Debug.Log("Loading in players");
            for (int i = 0; i < players.Length; i++)
            {
                players[i].transform.position = playerSpawns[i].position;
                GameObject stroller = Instantiate(strollerPrefab, playerSpawns[i].position + new Vector3(0, 0, 2f), Quaternion.identity);
                DetermineColor(players[i].GetComponent<CharacterMovementController>().GetColorName(), stroller);
                stroller.GetComponent<StrollerController>().SetID(players[i].GetComponent<CharacterMovementController>().getPlayerID());
                if (players[i].transform.transform.position == playerSpawns[i].position)
                {
                    spawnedPlayers = true;
                }
            }
        }
    }

    private void DetermineColor(string colorName, GameObject stroller)
    {
        Debug.Log(colorName);
        if (colorName.Equals("Blue"))
        {
            stroller.GetComponent<MeshRenderer>().material = strollerColor1;
        }
        else if (colorName.Equals("Green"))
        {
            stroller.GetComponent<MeshRenderer>().material = strollerColor2;
        }
        else if (colorName.Equals("Orange"))
        {
            Debug.Log("Orange Stroller");
            stroller.GetComponent<MeshRenderer>().material = strollerColor3;
        }
        else if (colorName.Equals("Yellow"))
        {
            stroller.GetComponent<MeshRenderer>().material = strollerColor4;
        }
        else if (colorName.Equals("Purple"))
        {
            stroller.GetComponent<MeshRenderer>().material = strollerColor5;
        }
    }
}
