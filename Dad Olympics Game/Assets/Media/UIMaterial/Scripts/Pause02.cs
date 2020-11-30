using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices.ComTypes;
//using System.Runtime.Remoting.Messaging;
using System.Threading;
//using System.Runtime.Hosting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause02 : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public GameObject OptionsMenuUI;
    public GameObject CreditsMenu;
    public static bool gameisPaused = false;

    public GameObject theImage;
    public GameObject theImage01;
    public GameObject theImage02;
    public GameObject theImage03;

    int menutracker = 0;


    PauseMenu thePause;

    public Text ResumeText;
    public Text MenuText;
    public Text OptionsText;
    public Text QuitText;

    public int newFontsize01;
    public int newFontsize02;

    public bool oneCycle = false;

    private float currentTime = 0f;
    private float startingTime = 1000f;

    private bool isOn = false;
    private bool isOnOptions = false;
    /*
    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
    }
    */
    // Start is called before the first frame update
    void Awake()
    {
        thePause = new PauseMenu();

        thePause.Menu.Pause.performed += ctx => Pause();

        thePause.Menu.Return.performed += ctx => Return();

        thePause.Menu.ScrollDown.performed += ctx => ScrollDown();

        thePause.Menu.ScrollUp.performed += ctx => ScrollUp();

        thePause.Menu.Go.performed += ctx => Go();

    }

    void Pause()
    {
     
        if (!gameisPaused)
        {
            //UnityEngine.Debug.Log("Testing");
            ResumeText.fontSize = newFontsize02;
            PauseMenuUI.SetActive(true);
            gameisPaused = true;
       
            Time.timeScale = 0f;

        }
        else
        {
            PauseMenuUI.SetActive(false);
            gameisPaused = false;
            Time.timeScale = 1f;
        }
    }

    void Return()
    {
        if (gameisPaused)
        {
            PauseMenuUI.SetActive(false);
            gameisPaused = false;
            Time.timeScale = 1f;
        }
    }

    void ScrollDown()
    {


        if (menutracker == 0)
        {
      
            theImage.SetActive(false);
            theImage01.SetActive(true);
 
        }
        
        else if (menutracker == 1)
        {
     
            theImage01.SetActive(false);
            theImage02.SetActive(true);
        }
        
        else if (menutracker == 2)
        {
     
            theImage02.SetActive(false);
            theImage03.SetActive(true);
           
        }
        
        else if (menutracker == 3)
        {
            theImage03.SetActive(false);
            theImage.SetActive(true);
            menutracker = -1;
        }
     
        
        menutracker++;
    }

    void Go()
    {
       
        if (menutracker == 0)
        {
            PauseMenuUI.SetActive(false);
            gameisPaused = false;
            Time.timeScale = 1f;
        }
        else if (menutracker == 1)
        {
            if (!isOnOptions)
            {
                OptionsMenuUI.SetActive(true);
                PauseMenuUI.SetActive(false);
                isOnOptions = true;
            }
            else
            {
                OptionsMenuUI.SetActive(false);
                PauseMenuUI.SetActive(true);
                isOnOptions = false;
            }
        }
        else if (menutracker == 2)
        {
            if (!isOn)
            {
                CreditsMenu.SetActive(true);
                PauseMenuUI.SetActive(false);
                isOn = true;
            }
            else
            {
                CreditsMenu.SetActive(false);
                PauseMenuUI.SetActive(true);
                isOn = false;
            }
        }
        else if (menutracker == 3)
        {
            Application.Quit();

        }
    }

    void ScrollUp()
    {
        if (menutracker == 1)
        {
            theImage.SetActive(true);
            theImage01.SetActive(false);
            menutracker = 0;
        }

        else if (menutracker == 2)
        {
            theImage01.SetActive(true);
            theImage02.SetActive(false);

            menutracker = 1;
        }

        else if (menutracker == 3)
        {
            theImage02.SetActive(true);
            theImage03.SetActive(false);
            menutracker = 2;
        }

      
        else if (menutracker == 0)
        {
            theImage03.SetActive(true);
            theImage.SetActive(false);
            menutracker = 3;

        }

        
    }

    void OnEnable()
    {
        thePause.Menu.Enable();
    }
    void OnDisable()
    {
        thePause.Menu.Disable();
    }

}
