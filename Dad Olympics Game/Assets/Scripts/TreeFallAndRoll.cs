using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFallAndRoll : MonoBehaviour
{
    public float WobbleTime;
    public float rollSpeed;
    public int AnimStage;
    public float lifetime;
    public bool falling = false;
    private float rolltime;
    private float StartWobble;

    private Transform StartPos;

    public Animator TreeAnim;
    public Rigidbody RB;

    private float StartTimer;
    //private float PercentageMoved;



    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform;
        rolltime = WobbleTime + lifetime;
        StartWobble = WobbleTime;
        RB = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        WobbleTime -= Time.deltaTime;

        rolltime -= Time.deltaTime;
        if(WobbleTime < 0)
        {
            if (!falling)
            {
                SetPhase1();
                falling = true;
                
            }
        }

        if (AnimStage == 2) {
            //transform.Rotate(Vector3.up * Time.deltaTime, Space.Self);
            //RB.AddForce(transform.forward * rollSpeed);
            transform.Translate(Vector3.forward * (Time.deltaTime * rollSpeed), Space.World);

            if (rolltime < 0)
            {
                print(rolltime);
                RB.velocity = Vector3.zero;
                RB.angularVelocity = Vector3.zero;
                falling = false;
                WobbleTime = StartWobble;
                TreeAnim.enabled = true;
                rolltime = StartWobble + lifetime;
                TreeAnim.SetInteger("Phase", 0);
                
                
            }
        }


    }

    public void SetPhase1()
    {
        AnimStage = 1;
        TreeAnim.SetInteger("Phase", AnimStage);
    }

    //Is called through the animator
    public void SetPhase2()
    {
        AnimStage = 2;
        TreeAnim.enabled = false;
    }
}
