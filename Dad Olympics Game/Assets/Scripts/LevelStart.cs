﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LevelStart : MonoBehaviour
{
    public GameObject startCam;

    public Text startText;

    private List<PlayerConfiguration> PlayerConfigs;

    //public GameObject GameManager;

    public void HandleGameStart(PlayerInput pi)
    {
        if(startCam != null)
        {
            Destroy(startCam);
            startText.text = "";
        }
        Debug.Log("Player Joined: " + pi.playerIndex);
        /**if (!PlayerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            PlayerConfigs.Add(new PlayerConfiguration(pi));
        }**/

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
}
