using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixItCommonRobotMovement : MonoBehaviour {

    public GameObject playerbody;
    public GameObject playerTarget;

    private Transform pttransform;
    private Health HealthScript;

    private bool panleft;
    private int frames;

	void Start () 
    {
        Vector3 robotstartpos = new Vector3(0, -1, 8);
        transform.position = robotstartpos;
        panleft = true;
        frames = 60;

        pttransform = playerTarget.GetComponent<Transform>();

        HealthScript = playerbody.GetComponentInParent<Health>();
    }
	
	// Update is called once per frame
	void Update () 
    {
        frames++;

        if (frames >= 60)
        {
            if (panleft)
            {
                //move left one square
                if (transform.position.x < 6)
                {
                    //Hold the values for where the robot will be once moved and the current player position
                    Vector3 futurerobotpos = new Vector3(transform.position.x + 2, 0f, transform.position.z);
                    Vector3 playerposition = new Vector3(pttransform.position.x, 0f, pttransform.position.z);

                    //If the player is not in the way, move the robot
                    if(futurerobotpos != playerposition)
                    {
                        var pos = transform.position;
                        pos.x += 2;
                        transform.position = pos;
                    }
                    //If the player is in the way, it takes damage
                    else
                    {
                        HealthScript.TakeDamage(10);
                    }

                    //If robot has reached the edge, switch directions
                    if (transform.position.x >= 6)
                    {
                        panleft = false;
                    }
                }
            }
            else
            {
                //move right one square
                if (transform.position.x > -8)
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
                    }

                    //If robot has reached the edge, switch directions
                    if (transform.position.x <= -8)
                    {
                        panleft = true;
                    }
                }
            }
            frames = 0;
        }
    }
}
