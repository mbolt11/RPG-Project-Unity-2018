using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRobotInstantiation : MonoBehaviour 
{
    //The locations were the new robots will be placed
    public GameObject[] spots;

    private int counter;
    private bool firstAwake = true;

    // Use this for initialization
    void Awake ()
    {
        if (OverworldGameController.gameInfo != null)
        {
            counter = 0;

            //Disable the robots that have been beaten
            for (int i = 0; i < OverworldGameController.gameInfo.robotsBeaten.Count; i++)
            {
                string todeactivate = OverworldGameController.gameInfo.robotsBeaten[i];
                Destroy(GameObject.Find(todeactivate));
            }

            firstAwake = false;
        }
	}

    public void Update()
    {
        if (firstAwake)
            Awake();
    }

    //Method to be called by OverworldRobotMovement script when a robot is destroyed
    public void CreateNewRobot(GameObject robot)
    {
        //Get the location/rotation of the new instantiation spot
        Vector3 location = spots[counter].transform.position;
        Quaternion rotation = spots[counter].transform.rotation;

        //give specific robot its same y position according to its scale
        location.y = robot.transform.position.y;

        //Instantiate the robot in the given spot
        Instantiate(robot,location,rotation,transform);

        if(counter < spots.Length-1)
        {
            counter++;
        }
        else
        {
            counter = 0;
        }
    }
}
