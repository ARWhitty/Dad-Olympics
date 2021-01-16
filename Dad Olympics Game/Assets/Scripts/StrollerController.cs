
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrollerController : MonoBehaviour
{
    public GameObject player, knockBack;

    public int StrollerID;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnableKnockBack() //Vector3 direction, bool dropStroller)
    {
        if(knockBack)
        {
            StartCoroutine(knockBackTimer());
        } else
        {
            Debug.Log("No knockback collider assigned to stroller controller");
        }
        /**Debug.Log("KBCalled");
        if (dropStroller)
            player.GetComponent<CharacterMovementController>().DropGrabbedItem();
        GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);**/
    }

    private IEnumerator knockBackTimer()
    {
        yield return new WaitForSeconds(0.1f);
        knockBack.SetActive(true);
        yield return new WaitForSeconds(1.5f); //Knockback enabled for 2 seconds when thrown.
        knockBack.SetActive(false);

    }

    public void SetID(int id)
    {
        StrollerID = id;
    }
}
