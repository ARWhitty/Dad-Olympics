using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkPointNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.Equals("Player"))
        {
            if(collider.gameObject.GetComponent<CharacterMovementController>().getCheckpointCount() == checkPointNum - 1)
            {
                collider.gameObject.GetComponent<CharacterMovementController>().SetCheckpointCount(checkPointNum);
            }

        }
    }
}
