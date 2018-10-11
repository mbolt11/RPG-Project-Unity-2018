using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrid : MonoBehaviour 
{
    //Create variable to hold gameobject on grid
    public GameObject playerTarget;
    public GameObject robotTarget;
    public GameObject player;
    private Transform robot;

    //Target position and grid size
    Vector3 playertargetPos;
    Vector3 robottargetPos;
    public float gridSize;

    private void Start()
    {
        robot = GameObject.Find("FixItRobot").transform.GetChild(0);
    }

    //Set the robot and players position to be at the true position of the target
    void LateUpdate ()
    {
        //Set the position of the player target
        playertargetPos.x = Mathf.Floor(playerTarget.transform.position.x / gridSize) * gridSize;
        playertargetPos.y = Mathf.Floor(playerTarget.transform.position.y / gridSize) * gridSize;
        playertargetPos.z = Mathf.Floor(playerTarget.transform.position.z / gridSize) * gridSize;

        //Set the position of the robot target
        robottargetPos.x = Mathf.Floor(robotTarget.transform.position.x / gridSize) * gridSize;
        robottargetPos.y = Mathf.Floor(robotTarget.transform.position.y / gridSize) * gridSize;
        robottargetPos.z = Mathf.Floor(robotTarget.transform.position.z / gridSize) * gridSize;

        //Set the player and robot to 
        player.transform.position = playertargetPos;
        robot.transform.position = robottargetPos;
    }
}
