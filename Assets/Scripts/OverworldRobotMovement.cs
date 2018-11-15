using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldRobotMovement : MonoBehaviour 
{
    private float robotSpeed = 2;
    private float bossSpeed = 2;
    private Rigidbody robotRB;
    private Transform robotTran;
    private int frames;
    private int turns;
    private int nextTurn;
    private bool once;
    private bool justMade;
    private bool backItUp;
    private bool exitTrigger;
    private Vector3 movement;

    private GameObject newRobotsGameobject;
    private NewRobotInstantiation nriScript;
    private GameObject player;


    // Use this for initialization
    void Start () 
    {
        //Get the NewRobotInstantiation script
        newRobotsGameobject = GameObject.Find("New Robot Instantiations");
        nriScript = newRobotsGameobject.GetComponent<NewRobotInstantiation>();

        frames = 0;
        once = true;
        justMade = true;
        nextTurn = 60;
        robotRB = GetComponent<Rigidbody>();

        if (GetComponent<RobotController>().isBoss)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        backItUp = false;
        exitTrigger = true;
        movement = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () 
    {
        //stop robots from tilting
        //transform.localEulerAngles = new Vector3(0f, transform.rotation.y, 0f);

        frames++;

        if (!GetComponent<RobotController>().isBoss)
        {
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
            if (turns > 5)
            {
                //Call a method which creates a new robot instatiation
                nriScript.CreateNewRobot(gameObject);

                //Destory this robot
                Destroy(gameObject);
            }
        }

        //move boss out of the spawn zone
        if(justMade && GetComponent<RobotController>().isBoss && frames <= 150)
        {
            if (once)
            {
                float rotation = Random.Range(0f, 360f);
                transform.Rotate(0, rotation, 0);
                once = false;
            }

            Vector3 movement = transform.forward * Time.deltaTime * robotSpeed;
            robotRB.MovePosition(robotRB.position + movement);

            if(frames > nextTurn)
                justMade = false;
        }

        if (!justMade && GetComponent<RobotController>().isBoss)
        {
            if (player == null)
                getPlayer();
        }

        //track player movement
        if (!justMade && GetComponent<RobotController>().isBoss && player.GetComponent<PlayerController>().playerInBossZone())
        {
            transform.LookAt(player.transform);

            bool left = false;
            bool right = false;
            bool front = false;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.A))
                {
                    right = true;
                }
                else
                {
                    right = false;
                }

                if (Input.GetKey(KeyCode.D))
                {
                    left = true;
                }
                else
                {
                    left = false;
                }

                front = true;
            }
            else
                front = false;

            if (left && !right)
                movement = -1 * transform.right * bossSpeed * Time.deltaTime;
            else if (right && !left)
                movement = 1 * transform.right * bossSpeed * Time.deltaTime;
            else
                movement = Vector3.zero;

            robotRB.MovePosition(robotRB.position + movement);

            if (front)
                movement = transform.forward * bossSpeed * Time.deltaTime;
            else
                movement = Vector3.zero;

            robotRB.MovePosition(robotRB.position + movement);
        }
    }

    private void getPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log("had to find player");
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
            //frames = 0;
            backItUp = true;
            exitTrigger = false;
            //Debug.Log(name + " turned around");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wall")
            exitTrigger = true;
    }

    private IEnumerator RandomWait()
    {
        int waittime = Random.Range(1, 4);
        yield return new WaitForSeconds(waittime);
        backItUp = false;
        if (once)
        {
            //Movement pattern for common robot
            if(this.tag == "Common Robot" && exitTrigger)
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
                    if(exitTrigger)
                        transform.Rotate(0, 180, 0);
                    robotSpeed *= 4;
                }
                else
                {
                    if(exitTrigger)
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

    //this probrably doesnt do anything--------------------------------------------------------------------------------
    private IEnumerator waitForScene()
    {
        yield return new WaitForSeconds(10);
    }
}
