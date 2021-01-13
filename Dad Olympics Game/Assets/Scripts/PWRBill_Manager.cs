using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PWRBill_Manager : MonoBehaviour
{
    //Electricity variables
    public int Score;
    public Text PowerTXT;

    //List of objects to interact with
    public List<Interaction> Interactives = new List<Interaction>();
    public int MaxObjects;
    private int[] randomPicks;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject Electronic in GameObject.FindGameObjectsWithTag("Electronics"))
        {
            Interactives.Add(Electronic.GetComponent<Interaction>());
        }

        for(int i = 0; i < MaxObjects; i++)
        { 
            randomPicks[i] =  Random.Range(0, Interactives.Count);            
        }

        for(int j = 0; j < randomPicks.Length; j++)
        {
            Interactives[randomPicks[j]].ToggleVisual();
        }
    }

    // Update is called once per frame
    void Update()
    {
        PowerTXT.text = "Power Bill: $" + Score;

    }

    public void AddScore(int toAdd)
    {
        Score += toAdd;

    }
}
