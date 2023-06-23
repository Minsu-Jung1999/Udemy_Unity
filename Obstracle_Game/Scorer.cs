using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    private int hittingTimes = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnCollisionEnter(Collision Other)
    {
        if(Other.gameObject.tag != "Hit")
        {
            hittingTimes++;
        }
        Debug.Log("You've bumped into thign this many times: " + hittingTimes);

    }
}
