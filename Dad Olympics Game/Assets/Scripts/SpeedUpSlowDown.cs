using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpSlowDown : MonoBehaviour
{
    [Range(0.0f, 2.0f)]
    public float SpeedAdjustment;

    private float originalSpeedGrab;
    private float originalSpeedNormal;

    private CharacterMovementController CMC;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



    }
    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            //GETTERS
            CMC = other.GetComponent<CharacterMovementController>();
            CMC.setMoveSpeed(CMC.getMoveSpeed() * SpeedAdjustment);
            originalSpeedNormal = CMC.moveSpeedNormal;
            originalSpeedGrab = CMC.moveSpeedGrab;
            //SETTERS
            //CMC.moveSpeedGrab = originalSpeedGrab * SpeedAdjustment;
            //CMC.moveSpeedNormal = originalSpeedNormal * SpeedAdjustment;



        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CMC = other.GetComponent<CharacterMovementController>();
            CMC.setMoveSpeed(originalSpeedNormal);
        }
    }
}
        

