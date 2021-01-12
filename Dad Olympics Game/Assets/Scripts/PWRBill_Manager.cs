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
    public GameObject[] Interactives;
    public int MaxObjects;
    private int[] randomPicks;
    





    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < MaxObjects; i++)
        { 
       
            randomPicks[i] =  Random.Range(0, Interactives.Length);
            
        }

        for(int j = 0; j < randomPicks.Length; j++)
        {
            //Interactives[randomPicks[j]] 
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
