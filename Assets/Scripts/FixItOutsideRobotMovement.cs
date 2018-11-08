using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixItOutsideRobotMovement : MonoBehaviour
{

    public GameObject playerbody;
    public GameObject playerTarget;
    private Transform pttransform;

    private int frames;
    private int movesPerSide;
    private int currIndex;
    private Transform currRobot;
    private Health HealthScript;


    void Start()
    {
        Vector3 robotstartpos = new Vector3(-8, -0.15f, 8);
        transform.position = robotstartpos;
        currIndex = 0;
        movesPerSide = 0;
        frames = 60;
        currRobot = GameObject.Find("Fix-It Robot").GetComponentInChildren<Transform>();

        pttransform = playerTarget.GetComponent<Transform>();

        HealthScript = playerbody.GetComponentInParent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        frames++;

        if(movesPerSide > 6)
        {
            //rotate 90 degrees
            currRobot.Rotate(0, 90f, 0);

            movesPerSide = 0;

            currIndex++;
            if (currIndex > 3)
                currIndex = 0;   
        }

        if (frames >= 60)
        {
            if (currIndex == 0)
            {
                //Hold the values for where the robot will be once moved and the current player position
                Vector3 futurerobotpos = new Vector3(transform.position.x + 2, 0f, transform.position.z);
                Vector3 playerposition = new Vector3(pttransform.position.x, 0f, pttransform.position.z);

                //If the player is not in the way, move the robot
                if (futurerobotpos != playerposition)
                {
                    var pos = transform.position;
                    pos.x += 2;
                    transform.position = pos;
                }
                //If the player is in the way, it takes damage
                else
                {
                    HealthScript.TakeDamage(10);
                    movesPerSide--;
                }

            }
            else if(currIndex == 1)
            {
                //Hold the values for where the robot will be once moved and the current player position
                Vector3 futurerobotpos = new Vector3(transform.position.x, 0f, transform.position.z - 2);
                Vector3 playerposition = new Vector3(pttransform.position.x, 0f, pttransform.position.z);

                //If the player is not in the way, move the robot
                if (futurerobotpos != playerposition)
                {
                    var pos = transform.position;
                    pos.z -= 2;
                    transform.position = pos;
                }
                //If the player is in the way, it takes damage
                else
                {
                    HealthScript.TakeDamage(10);
                    movesPerSide--;
                }
            }
            else if(currIndex == 2)
            {
                //Hold the values for where the robot will be once moved and the current player position
                Vector3 futurerobotpos = new Vector3(transform.position.x - 2, 0f, transform.position.z);
                Vector3 playerposition = new Vector3(pttransform.position.x, 0f, pttransform.position.z);

                //If the player is not in the way, move the robot
                if (futurerobotpos != playerposition)
                {
                    var pos = transform.position;
                    pos.x -= 2;
                    transform.position = pos;
                }
                //If the player is in the way, it takes damage
                else
                {
                    HealthScript.TakeDamage(10);
                    movesPerSide--;
                }
            }
            else
            {
                //Hold the values for where the robot will be once moved and the current player position
                Vector3 futurerobotpos = new Vector3(transform.position.x, 0f, transform.position.z + 2);
                Vector3 playerposition = new Vector3(pttransform.position.x, 0f, pttransform.position.z);

                //If the player is not in the way, move the robot
                if (futurerobotpos != playerposition)
                {
                    var pos = transform.position;
                    pos.z += 2;
                    transform.position = pos;
                }
                //If the player is in the way, it takes damage
                else
                {
                    HealthScript.TakeDamage(10);
                    movesPerSide--;
                }
            }

            frames = 0;
            movesPerSide++;
        }
    }
}
