using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithRobot : MonoBehaviour
{

    public bool hasCollided;
    public GameObject playerBody;

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
        else if(other.tag == "CommonPart")
        {
            Debug.Log("part hit");
            playerBody.transform.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (other.tag == "OutsidePart")
        {
            Debug.Log("part hit");
            playerBody.transform.GetComponent<Renderer>().material.color = Color.yellow;
        }
    }
}