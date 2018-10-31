using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RobotController : MonoBehaviour
{
    //Get the robot's main body cube
    public GameObject robotBody;

    [HideInInspector]
    public bool isBoss;

    // Use this for initialization
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int rand = Random.Range(0, 4);
        isBoss = rand == 0 ? true : false;

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

        //color robot if it's a boss
        if (isBoss)
        {
            robotBody.transform.GetComponent<Renderer>().material.color = new Color(.83f, .69f, .22f, 1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If a wrench hits the robot, it turns green to indicate it is fixed
        if (other.tag == "Wrench")
        {
            //Change the color
            robotBody.transform.GetComponent<Renderer>().material.color = Color.green;
            Destroy(other.gameObject);

            //stop throwing parts
            GetComponent<RobotThrowsParts>().enabled = false;

            //check if boss
            if(isBoss)
            {
                //DROP A TOOL HERE
                Debug.Log("Drop a Tool");
            }
        }
    }
}