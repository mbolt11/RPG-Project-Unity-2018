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
    private bool once;
    private bool backItUp;
    private bool exitTrigger;

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
        robotRB = GetComponent<Rigidbody>();

        if (tag == "Outside Robot")
        {
            transform.Rotate(0, 90, 0);
        }

        backItUp = false;
        exitTrigger = true;
    }
	
	// Update is called once per frame
	void Update () 
    {
        frames++;


        if (frames <= 180)
        {
            Vector3 movement = transform.forward * Time.deltaTime * robotSpeed;
            robotRB.MovePosition(robotRB.position + movement);
        }
        else if (frames > 180)
        {
            once = true;
            StartCoroutine(Wait());
        }

        //After the robot has turned and moved 5 times, its movement pattern is done
        if (turns > 3)
        {
            //Call a method which creates a new robot instatiation
            nriScript.CreateNewRobot(gameObject);

            //Destory this robot
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //maybe stop robot movement?
        if (other.tag == "Player")
        {
            StartCoroutine(waitForScene());
        }
        else if(!other.CompareTag("BossZone") && !backItUp)
        {
            transform.Rotate(0, 180f, 0);
            backItUp = true;
            exitTrigger = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wall")
            exitTrigger = true;
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        backItUp = false;
        if (once)
        {
            //Movement pattern for common robot
            if(this.tag == "Common Robot" && exitTrigger)
            {
                if(turns % 2 == 0)
                {
                    transform.Rotate(0, -90, 0);
                }
                else
                {
                    transform.Rotate(0, 90, 0);
                }
            }


            //Movement pattern for outside robot
            if(this.tag == "Outside Robot")
            {
                //Speed up or slow down the robot every other time they turn around
                if (turns % 2 == 0)
                {
                    if(exitTrigger)
                        transform.Rotate(0, 180, 0);
                    robotSpeed *= 4;
                }
                else
                {
                    if(exitTrigger)
                        transform.Rotate(0, 180, 0);
                    robotSpeed /= 4;
                }
            }

            //Increment/update variables
            turns++;
            frames = 0;
            once = false;
        }
    }

    //this probrably doesnt do anything--------------------------------------------------------------------------------
    private IEnumerator waitForScene()
    {
        yield return new WaitForSeconds(10);
    }
}
