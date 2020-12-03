using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InitializeLevel : MonoBehaviour
{
    public Transform[] playerSpawns;
    public GameObject playerPrefab;

    private bool spawnedPlayers = false;
    private float waitTime = 8f;
    private float currentTime = 0;

    public Material strollerColor1;
    public Material strollerColor2;
    public Material strollerColor3;
    public Material strollerColor4;
    public Material strollerColor5;

    private GameObject[] players;

    public GameObject strollerPrefab;

    public GameObject startCam;

    public Text startText;
    // Start is called before the first frame update
    void Start()
    {
        players = ManagePlayerHub.Instance.getPlayers().ToArray();
        PlayerInputManager.instance.DisableJoining();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > waitTime && !spawnedPlayers)
        {
            Destroy(startCam);
            startText.text = "";
            Debug.Log("Loading in players");
            for (int i = 0; i < players.Length; i++)
            {
                GameObject stroller = Instantiate(strollerPrefab, playerSpawns[i].position + new Vector3(0, 0, 2f), Quaternion.identity);
                DetermineColor(players[i].GetComponent<CharacterMovementController>().GetColorName(), stroller);
                stroller.GetComponent<StrollerController>().SetID(players[i].GetComponent<CharacterMovementController>().getPlayerID());
                spawnedPlayers = true;
            }
        }
        else if(!spawnedPlayers)
        {
            Debug.Log("Spawning player");
            for (int i = 0; i < players.Length; i++)
            {
                //Vector3 flagPos = GameObject.Find("Proto_Flag_01").transform.position;
                //players[i].transform.LookAt(new Vector3(transform.position.x, transform.position.y, transform.position.z));
                players[i].transform.forward = startCam.transform.forward;
                players[i].transform.position = playerSpawns[i].position;
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
