using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAward : MonoBehaviour
{
    public GameObject GoldStroller;

    public GameObject SilverGrill;

    public GameObject BronzeTrash;

    public GameObject RegularTrike;

    public GameObject spawnPoint1;

    public GameObject spawnPoint2;

    public GameObject spawnPoint3;

    public GameObject spawnPoint4;

    private int spawnCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            if (spawnCount == 0)
            {
                GameObject prize = Instantiate(GoldStroller, spawnPoint1.transform.position, spawnPoint1.transform.rotation) as GameObject;
                spawnCount++;
            }
            else if (spawnCount == 1)
            {
                GameObject prize = Instantiate(BronzeTrash, spawnPoint2.transform.position, spawnPoint2.transform.rotation) as GameObject;
                spawnCount++;
            }
            else if (spawnCount == 2)
            {
                GameObject prize = Instantiate(SilverGrill, spawnPoint3.transform.position, spawnPoint3.transform.rotation) as GameObject;
                spawnCount++;
            }
            else if (spawnCount == 3)
            {
                GameObject prize = Instantiate(RegularTrike, spawnPoint4.transform.position, spawnPoint4.transform.rotation) as GameObject;
                spawnCount++;
            }

        }
    }
}
