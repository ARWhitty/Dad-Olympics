using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ManagePlayerHub : MonoBehaviour
{
    public List<GameObject> players;

    private int playerIDCount = 0;

    public Material playerColor1;
    public Material playerColor2;
    public Material playerColor3;
    public Material playerColor4;

    public string colorName1;
    public string colorName2;
    public string colorName3;
    public string colorName4;

    public Text ReadyText;

    public Text StartText;

    private bool playerJoined;

    public static ManagePlayerHub Instance { get; private set; }

    private void Awake()
    {

    }

    public void SaveState()
    {
        if (Instance != null)
        {
            Debug.Log("Making a second instance of singleton very bad");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        players = new List<GameObject>();
        playerJoined = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerJoined)
        {
            StartText.text = "";
            int readyCount = 0;
            foreach (GameObject player in players)
            {
                if (player.GetComponent<CharacterMovementController>().GetReadyPlayer())
                {
                    readyCount++;
                }
            }
            ReadyText.text = "" + readyCount + "/" + players.Count + " players are ready! Stand on the start line to begin!";
        }

    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        playerJoined = true;
        players.Add(pi.gameObject);
        DontDestroyOnLoad(pi.gameObject);
        if (pi.gameObject.GetComponent<CharacterMovementController>())
        {
            pi.gameObject.GetComponent<CharacterMovementController>().SetPlayerID(playerIDCount);
            if (playerIDCount == 0)
            {
                pi.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = playerColor1;
                pi.gameObject.GetComponent<CharacterMovementController>().SetColorName(colorName1);
            }
            else if (playerIDCount == 1)
            {
                pi.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = playerColor2;
                pi.gameObject.GetComponent<CharacterMovementController>().SetColorName(colorName2);
            }
            else if (playerIDCount == 2)
            {
                pi.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = playerColor3;
                pi.gameObject.GetComponent<CharacterMovementController>().SetColorName(colorName3);
            }
            else if (playerIDCount == 3)
            {
                pi.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = playerColor4;
                pi.gameObject.GetComponent<CharacterMovementController>().SetColorName(colorName4);
            }

        }
        else if (pi.gameObject.GetComponent<CM_CharacterMovementController>())
            pi.gameObject.GetComponent<CM_CharacterMovementController>().SetPlayerID(playerIDCount);

        playerIDCount++;
    }

    public List<GameObject> getPlayers()
    {
        return players;
    }

    public void DeletePlayers()
    {
        foreach(GameObject player in players)
        {
            Destroy(player);
        }
    }
}
