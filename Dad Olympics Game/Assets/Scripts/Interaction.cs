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
    public GameObject Object_active;
    public GameObject Object_inactive;

    public int BillTotal = 0;
    public int TimerDelayAmount = 1;

    public Text BillTotalText;

    protected float CashTimer;

    private bool interactPressed = false;

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
                BillTotal++;
                BillTotalText.text = "Power Bill: $" + BillTotal;

            }
        }
    }

    public void ToggleVisual()
    {
        Object_active.SetActive(false);
        Object_inactive.SetActive(true);
        interactPressed = true;
    }
}
