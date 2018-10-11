using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithRobot : MonoBehaviour
{

    public bool hasCollided;

    // Use this for initialization
    void Start()
    {
        hasCollided = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Robot")
        {
            hasCollided = true;
        }
    }
}