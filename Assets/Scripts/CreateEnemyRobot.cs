using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemyRobot : MonoBehaviour {

    private OverworldGameController gameInfo;
    public Rigidbody CommonRobot;
    public Rigidbody OutsideRobot;
    private Vector3 start = new Vector3(0,0);
    private Quaternion target = Quaternion.Euler(0,0,0);
    private string enemyID;
	// Use this for initialization
	void Start () {

        if(GameObject.Find("GameController") != false)
        {
            gameInfo = GameObject.Find("GameController").GetComponent<OverworldGameController>().getSingleton();
            enemyID = gameInfo.getEnemyID();
            //Debug.Log("Enemy ID is " + enemyID);
        }
        else
        {
            enemyID = "Common Robot";
        }

        Rigidbody robotInstance;

        if(enemyID == CommonRobot.tag)
        {
            //Debug.Log("enemy created");
            robotInstance = Instantiate(CommonRobot, start, target) as Rigidbody;
            robotInstance.transform.parent = GameObject.Find("Fix-It Robot").transform;
        }
        else if(enemyID == OutsideRobot.tag)
        {
            //Debug.Log("enemy created");
            robotInstance = Instantiate(OutsideRobot, start, target) as Rigidbody;
            robotInstance.transform.parent = GameObject.Find("Fix-It Robot").transform;
        }            
	}
}
