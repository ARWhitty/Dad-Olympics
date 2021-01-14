using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour

{
    public PWRBill_Manager GameMaster;

    public int PowerCharge = 1;


    public GameObject Object_active;
    public GameObject Object_inactive;

    
    public int TimerDelayAmount = 1;

    public Text BillTotalText;

    protected float CashTimer;

    private bool interactPressed = false;

    private void Start()
    {
        GameMaster = GameObject.Find("Game Manager").GetComponent<PWRBill_Manager>();
    }

    public void Update()
    {
        if (interactPressed)
        {
            
        }

        if (!interactPressed)
        {
            CashTimer += Time.deltaTime;

            if (CashTimer >= TimerDelayAmount)
            {
                CashTimer = 0f;
                GameMaster.AddScore(PowerCharge);

            }
        }
    }

    public void ToggleVisual()
    {
        Object_active.SetActive(false);
        Object_inactive.SetActive(true);
        if (interactPressed == false)
        {
            GameMaster.NumItemsOn -= 1;
        }
        interactPressed = true;
    }
}
