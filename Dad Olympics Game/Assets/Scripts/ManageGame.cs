using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageGame : MonoBehaviour
{

    private List<PlayerConfiguration> configs;

    public GameObject[] checkpoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collided with end");
        if (collider.tag.Equals("Player"))
        {
            Debug.Log("Player checkpoint: " + collider.gameObject.GetComponent<CharacterMovementController>().getCheckpointCount());
            if(collider.gameObject.GetComponent<CharacterMovementController>().getCheckpointCount() == checkpoints.Length)
            {
                Debug.Log("Finished!");
            }
        }
    }

    public void updatePlayerConfigsList(List<PlayerConfiguration> configs)
    {
        foreach(PlayerConfiguration config in configs)
        {
            if (!this.configs.Contains(config))
            {
                this.configs.Add(config);
            }
        }
    }
}
