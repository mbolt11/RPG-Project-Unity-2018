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
        StartCoroutine(RandomWait());
	}
	
	// Update is called once per frame
	void Update () 
    {
        frames++;

        if (frames <= 50)
        {
            Vector3 movement = transform.forward * Time.deltaTime * commonRobotSpeed;
            robotRB.MovePosition(robotRB.position + movement);
        }
        else if (frames > 50)
        {
            if (transform.rotation.y < 45)
            {
                robotRB.MoveRotation(Quaternion.Euler(0, 45, 0));
                StartCoroutine(RandomWait());
            }
            else
            {
                robotRB.MoveRotation(Quaternion.Euler(0, -45, 0));
                StartCoroutine(RandomWait());
            }
                
            turns++;
            frames = 0;
        }

        //After the robot has turned and moved 3 times, its movement pattern is done
        if(turns > 4)
        {
            //Call a method which creates a new robot instatiation somewhere?

            //Destory this robot
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wall")
        {
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    private IEnumerator RandomWait()
    {
        int waittime = Random.Range(0, 3);
        yield return new WaitForSeconds(waittime);
    }
}
