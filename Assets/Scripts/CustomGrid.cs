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
    private OverworldGameController gameInfo;
    private string enemyID;

    //Target position and grid size
    Vector3 playertargetPos;
    Vector3 robottargetPos;
    public float gridSize;

    private void Start()
    {
        if (GameObject.Find("GameController") != false)
        {
            gameInfo = GameObject.Find("GameController").GetComponent<OverworldGameController>().getSingleton();
            enemyID = gameInfo.getEnemyID();
        }
        else
        {
            enemyID = "Common Robot";
        }

        if (enemyID == "Outside Robot")
            robotTarget.transform.position = new Vector3(-8, 0, 8);
    }

    //Set the robot and players position to be at the true position of the target
    void LateUpdate ()
    {
        //Set the position of the player target
        playertargetPos.x = Mathf.Floor(playerTarget.transform.position.x / gridSize) * gridSize;
        playertargetPos.y = Mathf.Floor(playerTarget.transform.position.y / gridSize) * gridSize;
        playertargetPos.z = Mathf.Floor(playerTarget.transform.position.z / gridSize) * gridSize;

        //Set the player and robot to 
        player.transform.position = playertargetPos;

       if (robot != null)
        {
            //Set the position of the robot target
            robottargetPos.x = Mathf.Floor(robotTarget.transform.position.x / gridSize) * gridSize;
            //robottargetPos.y = (Mathf.Floor(robotTarget.transform.position.y / gridSize) * gridSize) + 0.5f;
            robottargetPos.z = Mathf.Floor(robotTarget.transform.position.z / gridSize) * gridSize;

            robot.transform.position = robottargetPos;
        }
        else
        {
            GameObject fix = GameObject.Find("Fix-It Robot");
            Debug.Log("Trying to assign child" + fix);
            robot = GameObject.Find("Fix-It Robot").transform.GetChild(0);
        }
    }
}
