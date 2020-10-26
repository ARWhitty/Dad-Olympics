using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireHydrantKB : MonoBehaviour
{
    public bool Spraying = false;
    public float delay;
    public float length;
    private float timer;
    public short force;

    public GameObject waterKB;
    public ParticleSystem WaterEffect;

    // Start is called before the first frame update
    void Start()
    {
        timer = delay;
    }

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;
        if (delay < 0)
        {
            Spraying = !Spraying;
            delay = timer;
        }

        if (Spraying)
        {
            waterKB.SetActive(true);
            waterKB.GetComponent<KnockBack>().kBForce = force;
            WaterEffect.Play();
            

        }
        else
        {
            waterKB.SetActive(false);
            waterKB.GetComponent<KnockBack>().kBForce = 0;
            WaterEffect.Stop();
        }
    }
}
