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

public class Pause01 : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public static bool gameisPaused = false;


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

    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
    }

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
      
            ResumeText.fontSize = newFontsize01;
            MenuText.fontSize = newFontsize02;
 
        }
        
        else if (menutracker == 1)
        {
            MenuText.fontSize = newFontsize01;
            OptionsText.fontSize = newFontsize02;
         

        }
        
        else if (menutracker == 2)
        {
            OptionsText.fontSize = newFontsize01;
            QuitText.fontSize = newFontsize02;
           
        }
        
        else if (menutracker == 3)
        {
            QuitText.fontSize = newFontsize01;
            ResumeText.fontSize = newFontsize02;
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
          
        }
        else if (menutracker == 2)
        {
           
        }
        else if (menutracker == 3)
        {
            SceneManager.LoadScene("TitleScreen");

        }
    }

    void ScrollUp()
    {
        if (menutracker == 1)
        {
           
            ResumeText.fontSize = newFontsize02;
            MenuText.fontSize = newFontsize01;
            menutracker = 0;
        }

        else if (menutracker == 2)
        {
            MenuText.fontSize = newFontsize02;
            OptionsText.fontSize = newFontsize01;

            menutracker = 1;
        }

        else if (menutracker == 3)
        {
            OptionsText.fontSize = newFontsize02;
            QuitText.fontSize = newFontsize01;
            menutracker = 2;
        }

      
        else if (menutracker == 0)
        {
            QuitText.fontSize = newFontsize02;
            ResumeText.fontSize = newFontsize01;
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
