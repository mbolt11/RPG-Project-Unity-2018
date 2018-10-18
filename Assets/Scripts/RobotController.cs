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
        //If we are in the overworld
        if(currentScene.name == "Overworld")
        {
            GetComponent<RobotThrowsParts>().enabled = false;
        }
        //If we are in the Fix It World
        else
        {
            GetComponent<RobotThrowsParts>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If a wrench hits the robot, it turns green to indicate it is fixed
        if (other.tag == "Wrench")
        {
            //Change the color
            robotBody.transform.GetComponent<Renderer>().material.color = Color.green;

            //stop throwing parts
            GetComponent<RobotThrowsParts>().enabled = false;
        }
    }
}
