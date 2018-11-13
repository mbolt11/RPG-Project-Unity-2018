using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonRobotOverworldMovement : MonoBehaviour 
{
    public float commonRobotSpeed = 0.005f;

    private Rigidbody robotRB;
    private int frames;
    private int turns;


    // Use this for initialization
    void Start () 
    {
        frames = 0;
        robotRB = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        frames++;

        if (frames <= 100)
        {
            Vector3 movement = transform.forward * Time.deltaTime * commonRobotSpeed;
            robotRB.MovePosition(robotRB.position + movement);
        }
        else if (frames > 100)
        {
            if (transform.rotation.y < 45)
            {
                transform.Rotate(new Vector3(0, 45, 0));
            }
            else
            {
                transform.Rotate(new Vector3(0, -45, 0));
            }
                
            turns++;
            frames = 0;
        }

        //After the robot has turned and moved 3 times, its movement pattern is done
        if(turns > 3)
        {
            //Call a method which creates a new robot instatiation somewhere?

            //Destory this robot
            Destroy(gameObject);
        }
    }
}
