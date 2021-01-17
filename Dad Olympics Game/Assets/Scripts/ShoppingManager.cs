using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingManager : MonoBehaviour
{
    public List<GameObject> PrefabsToSpawn = new List<GameObject>();

    private List<ShoppingSpawner> Spawners = new List<ShoppingSpawner>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject SpawnLocation in GameObject.FindGameObjectsWithTag("RandomPick"))
        {
            Spawners.Add(SpawnLocation.GetComponent<ShoppingSpawner>());
        }

        for(int i = 0; i < Spawners.Count; i++)
        {
            //INCOMPLETE need to randomly pick one of PrefabToSpawn and check it hasn't already been spawned
            //Spawners[i].OBJtoSpawn =
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
