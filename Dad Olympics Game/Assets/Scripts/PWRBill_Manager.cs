using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PWRBill_Manager : MonoBehaviour
{
    //Electricity variables
    public int Score;
    public Text PowerTXT;
    public int NumItemsOn;
    public Text ItemsOnTXT;

    //List of objects to interact with
    public List<Interaction> Interactives = new List<Interaction>();
    public int MaxObjectsOff;


    // Start is called before the first frame update
    void Start()
    {

        foreach (GameObject Electronic in GameObject.FindGameObjectsWithTag("RandomPick"))
        {
            Interactives.Add(Electronic.GetComponent<Interaction>());
            
        }
        NumItemsOn = Interactives.Count;

        for (int i = 0; i < MaxObjectsOff; i++)
        {
            int tmp = Random.Range(0, Interactives.Count);
            Interactives[tmp].ToggleVisual();
            //RandomPicks.Add(Interactives[tmp]);

        }
    }

    // Update is called once per frame
    void Update()
    {
        PowerTXT.text = "Power Bill: $" + Score;
        ItemsOnTXT.text = "Electronics: " + NumItemsOn;


    }

    public void AddScore(int toAdd)
    {
        Score += toAdd;

    }
}
