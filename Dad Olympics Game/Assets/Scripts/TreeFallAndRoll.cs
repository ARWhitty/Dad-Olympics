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
            transform.Rotate(this.transform.up, Time.deltaTime * rollSpeed);
            //RB.AddForce(transform.forward * rollSpeed);

            if (rolltime < 0)
            {
                TreeAnim.enabled = true;
                TreeAnim.SetInteger("Phase", 0);
            }
        }


    }

    public void SetPhase1()
    {
        AnimStage = 1;
        TreeAnim.SetInteger("Phase", AnimStage);
    }
    public void SetPhase2()
    {
        AnimStage = 2;
        TreeAnim.enabled = false;
    }
}
