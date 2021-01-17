using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPicker : MonoBehaviour
{
    public int MaxDespawned;
    public List<GameObject> ToBePicked = new List<GameObject>();
    private List<int> picks = new List<int>();



    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < MaxDespawned; i++)
        {
            ValidatePicks();
        }

        for(int j = 0; j < MaxDespawned; j++)
        {
            ToBePicked[picks[j]].SetActive(false);
        }

        
    }

    //Picks and checks which objects to spawn
    void ValidatePicks()
    {
        int picked = Random.Range(0, ToBePicked.Count);
        while(picks.Contains(picked))
        {
            picked = Random.Range(0, ToBePicked.Count);
        }
        picks.Add(picked);        

    }
}
