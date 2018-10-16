using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixItPlayerMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject robotTarget;
    private Rigidbody rb;
    private Transform rtTransform;

    private void Start()
    {
        //Set player target start position
        Vector3 playerstartpos = new Vector3(-8, 0, -6);
        transform.position = playerstartpos;

        //Set player start rotation
        player.transform.rotation = Quaternion.Euler(0, 0, 0);

        //Get the player rigidbody and the player collision script
        rb = player.GetComponent<Rigidbody>();

        //Get the robot target transform
        rtTransform = robotTarget.GetComponent<Transform>();
    }

    // Move the player target by increments of 2 on button down
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            //Hold the value for where the player will be once moved
            Vector3 futureplayerpos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);

            //Get the current robot target transform position
            Vector3 robotposition = new Vector3(rtTransform.position.x, rtTransform.position.y, rtTransform.position.z);

            //Check if movement will put player past the edge
            if (transform.position.z < 8)
            {
                //Check if movement will put the player where the robot is
                if (futureplayerpos != robotposition)
                {
                    //Move the player foward 2 units
                    var pos = transform.position;
                    pos.z += 2;
                    transform.position = pos;
                }
            }
            //Rotate the way the player is facing toward front
            rb.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        if (Input.GetKeyDown("a"))
        {
            //Hold the value for where the player will be once moved
            Vector3 futureplayerpos = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z);

            //Get the current robot target transform position
            Vector3 robotposition = new Vector3(rtTransform.position.x, rtTransform.position.y, rtTransform.position.z);

            //Check if the movement will put the player past the edge
            if (transform.position.x > -8)
            {
                //Check if the movement will put the player where the robot is
                if (futureplayerpos != robotposition)
                {
                    //Move the player left 2 units
                    var pos = transform.position;
                    pos.x -= 2;
                    transform.position = pos;
                }
            }
            //Rotate the way the player is facing toward the left
            rb.rotation = Quaternion.Euler(0f, -90f, 0f);
        }

        if (Input.GetKeyDown("s"))
        {
            //Hold the value for where the player will be once moved
            Vector3 futureplayerpos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);

            //Get the current robot target transform position
            Vector3 robotposition = new Vector3(rtTransform.position.x, rtTransform.position.y, rtTransform.position.z);

            //Check if the movement will put the player past the edge
            if (transform.position.z > -6)
            {
                //Check if the movement will put the player where the robot is
                if (futureplayerpos != robotposition)
                {
                    //Move the player backward 2 units
                    var pos = transform.position;
                    pos.z -= 2;
                    transform.position = pos;
                }
            }
            //Rotate the way the player is facing toward back
            rb.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        if (Input.GetKeyDown("d"))
        {
            //Hold the value for where the player will be once moved
            Vector3 futureplayerpos = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);

            //Get the current robot target transform position
            Vector3 robotposition = new Vector3(rtTransform.position.x, rtTransform.position.y, rtTransform.position.z);

            //Check if the movement will put the player past the edge
            if (transform.position.x < 6)
            {
                //Check if the movement will put the player where the robot is
                if (futureplayerpos != robotposition)
                {
                    //Move the player right 2 units
                    var pos = transform.position;
                    pos.x += 2;
                    transform.position = pos;
                }
            }
            //Rotate the way the player is facing toward the right
            rb.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
    }
}

