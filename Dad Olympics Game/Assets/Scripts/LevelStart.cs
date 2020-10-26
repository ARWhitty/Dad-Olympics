using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStart : MonoBehaviour
{
    public GameObject startCam;

    public Text startText;

    public void HandleGameStart()
    {
        if(startCam != null)
        {
            Destroy(startCam);
            startText.text = "";
        }

    }
}
