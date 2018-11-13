using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonRobotOverworldMovement : MonoBehaviour 
{
    public float commonRobotSpeed = 0.005f;

    private Rigidbody robotRB;
    private Transform robotTran;
    private int frames;
    private int turns;
    private int nextTurn;
    private bool once;


    // Use this for initialization
    void Start () 
    {
        frames = 60;
        once = true;
        nextTurn = 60;
        robotRB = GetComponent<Rigidbody>();
        StartCoroutine(RandomWait()); //what is this for???
	}
	
	// Update is called once per frame
	void Update () 
    {
        frames++;

        if (frames <= nextTurn)
        {
            Vector3 movement = transform.forward * Time.deltaTime * commonRobotSpeed;
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
            //Call a method which creates a new robot instatiation somewhere?

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
        Debug.Log(waittime);
        yield return new WaitForSeconds(waittime);

        if (once)
        {
            float rotation = Random.Range(0f, 360f);
            transform.Rotate(0, rotation, 0);
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
