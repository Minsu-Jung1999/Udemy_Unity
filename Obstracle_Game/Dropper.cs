using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    MeshRenderer renderer;
    Rigidbody rigid;
    [SerializeField] float timeToWait = 5f;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        rigid = GetComponent<Rigidbody>();

        renderer.enabled = false;
        rigid.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timeToWait)
        {
            renderer.enabled = true;
            rigid.useGravity = true;
        }
    }
}
