using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PopUpCutout : MonoBehaviour
{
    public Quaternion currentPos;
    public Quaternion rotationAngle;
    public float speed;
    public bool Standing;
    public float timer;
    private float countdown;
    private short initialForce;

    // Start is called before the first frame update
    void Start()
    {
        countdown = timer;
        initialForce = this.GetComponent<KnockBack>().kBForce;
           currentPos = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //print(countdown);
         countdown -= Time.deltaTime;
        if (countdown < 0) {
            Standing = !Standing;
            countdown = timer;
            }

        if (Standing)
        {
            this.GetComponent<KnockBack>().kBForce = 0;
            this.GetComponent<BoxCollider>().enabled = false;
            Quaternion wantedRotation = transform.rotation * Quaternion.AngleAxis(10, Vector3.left);
            transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime*speed);
        }
        else
        {
            this.GetComponent<KnockBack>().kBForce = initialForce;
            this.GetComponent<BoxCollider>().enabled = true;
            transform.rotation = Quaternion.Slerp(transform.rotation, currentPos, Time.deltaTime * speed);
        }

    }

}
