using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubColorChange : MonoBehaviour
{
    public Material playerColor;

    public GameObject gameManager;

    public string colorName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        List<GameObject> players = gameManager.GetComponent<ManagePlayerHub>().getPlayers();
        if (other.tag.Equals("Player"))
        {
            bool isTaken = false;
            foreach(GameObject player in players)
            {
                if (player.GetComponent<CharacterMovementController>().GetColorName().Equals(colorName))
                {
                    isTaken = true;
                }
            }
            if (!isTaken)
            {
                other.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = playerColor;
                other.GetComponent<CharacterMovementController>().SetColorName(colorName);
            }
        }
    }
}
