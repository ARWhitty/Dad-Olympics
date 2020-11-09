using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateObject : MonoBehaviour
{
    public Material[] animationMaterials;
    public float swapTime;
    public GameObject objectToAnimate;

    private int materialCounter;
    private float timeCount;
    // Start is called before the first frame update
    void Start()
    {
        if(animationMaterials.Length > 0)
        {
            objectToAnimate.GetComponent<MeshRenderer>().material = animationMaterials[0];
            materialCounter++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
        if(timeCount >= swapTime)
        {
            if(animationMaterials.Length <= materialCounter)
            {
                objectToAnimate.GetComponent<MeshRenderer>().material = animationMaterials[0];
                materialCounter = 1;
            }
            else
            {
                objectToAnimate.GetComponent<MeshRenderer>().material = animationMaterials[materialCounter];
                materialCounter++;
            }
            timeCount = 0;
        }
    }
}
