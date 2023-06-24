using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collsion");
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");

    }
}
