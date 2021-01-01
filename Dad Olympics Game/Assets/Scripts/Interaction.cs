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

    private bool interactPressed = false;

    public void Update()
    {
        if (interactPressed)
        {
            
        }
    }

    public void ToggleVisual()
    {
        Object_active.SetActive(false);
        Object_inactive.SetActive(true);
    }
}
