using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManholeGeiser : MonoBehaviour
{

    public bool up = false;
    public float speed;
    public float delay;
    public float length;

    public GameObject water;
    public GameObject cover;

    public float size;

    private Transform waterBaseT;
    private Vector3 coverBaseT;
    public Vector3 coverTargetOffset;

    // Start is called before the first frame update
    void Awake()
    {
        waterBaseT = water.transform;
        coverBaseT = cover.transform.position;

        //SprayUp();
    }

    // Update is called once per frame
    void Update()
    {
        if (up)
        {
            water.transform.localScale = Vector3.Lerp(water.transform.localScale, new Vector3(1, size, 1), Time.deltaTime * speed);
            cover.transform.position = Vector3.Lerp(cover.transform.position, coverTargetOffset,Time.deltaTime * speed);
        }
        else
        {
            water.transform.localScale = Vector3.Lerp(water.transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * speed); 
            cover.transform.position = Vector3.Lerp(cover.transform.position, coverBaseT, Time.deltaTime * speed);
            print(coverBaseT);
        }
    }

    void SprayUp()
    { float timer = 0;
        while(timer < length)
        {
            timer += Time.deltaTime;

            water.transform.localScale = Vector3.Lerp(water.transform.localScale, new Vector3(0, size, 0), Time.deltaTime * speed);
        }
    }
}
