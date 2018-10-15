using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotTargetController : MonoBehaviour {

    private OverworldGameController gameInfo;
    private string enemyID;

    // Use this for initialization
    void Start () {

        if (GameObject.Find("GameController") != false)
        {
            gameInfo = GameObject.Find("GameController").GetComponent<OverworldGameController>().getSingleton();
            enemyID = gameInfo.getEnemyID();
            //Debug.Log("Enemy ID is " + enemyID);
        }
        else
        {
            enemyID = "Common Robot";
        }

        if(enemyID == "Common Robot")
        {
            GetComponent<FixItCommonRobotMovement>().enabled = true;
            GetComponent<FixItOutsideRobotMovement>().enabled = false;
        }
        else
        {
            GetComponent<FixItCommonRobotMovement>().enabled = false;
            GetComponent<FixItOutsideRobotMovement>().enabled = true;
        }
    }
}
