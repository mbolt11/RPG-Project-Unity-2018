using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldRobotMovement : MonoBehaviour 
{
    private float robotSpeed = 2;
    private Rigidbody robotRB;
    private Transform robotTran;
    private int frames;
    private int turns;
    private int nextTurn;
    private bool once;

    private GameObject newRobotsGameobject;
    private NewRobotInstantiation nriScript;


    // Use this for initialization
    void Start () 
    {
        //Get the NewRobotInstantiation script
        newRobotsGameobject = GameObject.Find("New Robot Instantiations");
        nriScript = newRobotsGameobject.GetComponent<NewRobotInstantiation>();

        frames = 0;
        once = true;
        nextTurn = 60;
        robotRB = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        frames++;

        if (frames <= nextTurn)
        {
            Vector3 movement = transform.forward * Time.deltaTime * robotSpeed;
            robotRB.MovePosition(robotRB.position + movement);
        }
        else if (frames > nextTurn)
        {
            once = true;
            StartCoroutine(RandomWait());
        }

        //After the robot has turned and moved 5 times, its movement pattern is done
        if(turns > 5)
        {
            //Call a method which creates a new robot instatiation
            nriScript.CreateNewRobot(gameObject);

            //Destory this robot
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(waitForScene());
        }
        else
        {
            transform.Rotate(0, 180f, 0);
        }
    }

    private IEnumerator RandomWait()
    {
        int waittime = Random.Range(1, 4);
        yield return new WaitForSeconds(waittime);

        if (once)
        {
            //Movement pattern for common robot
            if(this.tag == "Common Robot")
            {
                //Turn the robot a random amount
                float rotation = Random.Range(0f, 360f);
                transform.Rotate(0, rotation, 0);
            }


            //Movement pattern for outside robot
            if(this.tag == "Outside Robot")
            {
                //Speed up or slow down the robot every other time they turn around
                if (turns % 2 == 0)
                {
                    transform.Rotate(0, 180, 0);
                    robotSpeed *= 4;
                }
                else
                {
                    transform.Rotate(0, 90, 0);
                    robotSpeed /= 4;
                }
                    
            }

            //Increment/update variables
            turns++;
            nextTurn = Random.Range(120, 300);
            frames = 0;
            once = false;
        }
    }

    private IEnumerator waitForScene()
    {
        yield return new WaitForSeconds(10);
    }
}
