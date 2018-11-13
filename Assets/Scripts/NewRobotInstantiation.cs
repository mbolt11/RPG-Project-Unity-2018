using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRobotInstantiation : MonoBehaviour {

    //The locations were the new robots will be placed
    public GameObject[] spots;
    private int counter;

    // Use this for initialization
    void Start ()
    {
        counter = 0;
	}
	
    //Method to be called by OverworldRobotMovement script when a robot is destroyed
    public void CreateNewRobot(GameObject robot)
    {
        //Get the location/rotation of the new instantiation spot
        Vector3 location = spots[counter].transform.position;
        int yrot = Random.Range(0, 360);
        Quaternion rotation = Quaternion.Euler(0, yrot, 0);

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
