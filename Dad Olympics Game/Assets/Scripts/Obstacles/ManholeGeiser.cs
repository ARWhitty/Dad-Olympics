using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class ManholeGeiser : MonoBehaviour
{

    public bool up = false;
    public float speed;
    public float delay;
    public float length;
    private float timer;

    public GameObject water;
    public GameObject cover;

    public float size;

    private Transform waterBaseT;
    public Vector3 coverBaseT;
    public Vector3 coverTargetOffset;
    public Vector3 angle;

    // Start is called before the first frame update
    void Awake()
    {
        angle = this.transform.eulerAngles;
        waterBaseT = water.transform;
        coverBaseT = cover.transform.position;
        timer = delay;
    }

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;
        if (delay < 0)
        {
            up = !up;
            delay = timer;
        }

        if (up)
        {
            water.GetComponent<KnockBack>().kBForce = 10;
            float height = length/1.5f;
            water.transform.localScale = Vector3.Lerp(water.transform.localScale, new Vector3(1, size, 1), Time.deltaTime * speed);
            cover.transform.localPosition = Vector3.Lerp(cover.transform.localPosition, coverTargetOffset,Time.deltaTime * speed);
        }
        else
        {
            water.transform.localScale = Vector3.Lerp(water.transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * speed); 
            cover.transform.position = Vector3.Lerp(cover.transform.position, coverBaseT, Time.deltaTime * speed);
            water.GetComponent<KnockBack>().kBForce = 0;
        }


    }

}
