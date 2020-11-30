using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class ManholeGeiser : MonoBehaviour
{

    public bool up = false;
    public float speed;
    public float delay;
    public float length;
    private float timer;

    public GameObject waterCol;
    public ParticleSystem water;
    public GameObject cover;
    public VirtualAudioSource virtualAudioSource;

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
            if(up)
            {
                virtualAudioSource.enabled = false;
            } else
            {
                virtualAudioSource.enabled = true;
            }
            up = !up;
            delay = timer;
        }

        if (up)
        {
            waterCol.GetComponent<KnockBack>().kBForce = 10;
            water.Play();
            //float height = length/1.5f;
            //water.transform.localScale = Vector3.Lerp(water.transform.localScale, new Vector3(1, size, 1), Time.deltaTime * speed);
            waterCol.SetActive(true);
            cover.transform.localPosition = Vector3.Lerp(cover.transform.localPosition, coverTargetOffset,Time.deltaTime * speed);
        }
        else
        {
            //water.transform.localScale = Vector3.Lerp(water.transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * speed); 
            water.Stop();
            cover.transform.position = Vector3.Lerp(cover.transform.position, coverBaseT, Time.deltaTime * speed);
            waterCol.SetActive(false);
            waterCol.GetComponent<KnockBack>().kBForce = 0;

        }


    }

}
