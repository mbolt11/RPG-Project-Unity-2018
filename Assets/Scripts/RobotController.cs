using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    //Get the robot's main body cube
    public GameObject robotBody;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //If a wrench hits the robot, it turns blue to indicate it is fixed
        if (other.tag == "Wrench")
        {
            //Put the robot back to his position (we don't want him to be moved by the wrench

            //Change the color
            robotBody.transform.GetComponent<Renderer>().material.color = Color.green;
        }
    }
}
