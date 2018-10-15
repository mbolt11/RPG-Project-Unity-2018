using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RobotController : MonoBehaviour
{
    //Get the robot's main body cube
    public GameObject robotBody;

    // Use this for initialization
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "Overworld")
        {
            if(tag == "Common Robot")
                GetComponent<FixItCommonRobotMovement>().enabled = false;
            else
                GetComponent<FixItOutsideRobotMovement>().enabled = false;
            GetComponent<RobotThrowsParts>().enabled = false;
            //Debug.Log("Scripts Disabled");
        }
        else
        {
            if (tag == "Common Robot")
                GetComponent<FixItCommonRobotMovement>().enabled = true;
            else
                GetComponent<FixItOutsideRobotMovement>().enabled = true;
            GetComponent<RobotThrowsParts>().enabled = true;
        }
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
