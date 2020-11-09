using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/*** currently not using this system
public class PlayerConfigurationManager : MonoBehaviour
{
    private List<PlayerConfiguration> PlayerConfigs;

    public int MaxPlayers = 2;

    public static PlayerConfigurationManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("Making a second instance of singleton very bad");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            PlayerConfigs = new List<PlayerConfiguration>();
        }
    }

    public void SetPlayerColor(int index, Material color)
    {
        PlayerConfigs[index].PlayerMaterial = color;
    }

    public void ReadyPlayer(int index)
    {
        PlayerConfigs[index].IsReady = true;
        if(PlayerConfigs.Count == MaxPlayers && PlayerConfigs.All(p => p.IsReady == true))
        {
            SceneManager.LoadScene("KieranScene");
        }
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player Joined: " + pi.playerIndex);
        if(!PlayerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            PlayerConfigs.Add(new PlayerConfiguration(pi));
        }
    }
}


public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }

    public PlayerInput Input { get; set; }

    public int PlayerIndex { get; set; }

    public bool IsReady { get; set; }

    public Material PlayerMaterial { get; set; }
}**/