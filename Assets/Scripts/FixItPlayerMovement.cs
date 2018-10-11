using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixItPlayerMovement : MonoBehaviour
{
    public GameObject player;
    private Rigidbody rb;

    private CollisionWithRobot collisionscript;
    private char lastkeypressed;

    private void Start()
    {
        //Set player target start position
        Vector3 playerstartpos = new Vector3(-8, 0, -6);
        transform.position = playerstartpos;

        //Set player start rotation
        player.transform.rotation = Quaternion.Euler(0, 0, 0);

        //Get the player rigidbody and the player collision script
        rb = player.GetComponent<Rigidbody>();
        collisionscript = player.GetComponent<CollisionWithRobot>();
    }

    // Move the player target by increments of 2 on button down
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            //Move the player target only if it won't go past the edge
            if (transform.position.z < 8)
            {
                var pos = transform.position;
                pos.z += 2;
                transform.position = pos;
            }
            //Rotate the way the player is facing toward front
            rb.rotation = Quaternion.Euler(0f, 0f, 0f);
            lastkeypressed = 'w';
        }

        if (Input.GetKeyDown("a"))
        {
            //Move the player target only if it won't go past the edge
            if (transform.position.x > -8)
            {
                var pos = transform.position;
                pos.x -= 2;
                transform.position = pos;
            }
            //Rotate the way the player is facing toward the left
            rb.rotation = Quaternion.Euler(0f, -90f, 0f);
            lastkeypressed = 'a';
        }

        if (Input.GetKeyDown("s"))
        {
            //Move the player target only if it won't go past the edge
            if (transform.position.z > -6)
            {
                var pos = transform.position;
                pos.z -= 2;
                transform.position = pos;
            }
            //Rotate the way the player is facing toward back
            rb.rotation = Quaternion.Euler(0f, 180f, 0f);
            lastkeypressed = 's';
        }

        if (Input.GetKeyDown("d"))
        {
            //Move the player target only if it won't go past the edge
            if (transform.position.x < 6)
            {
                var pos = transform.position;
                pos.x += 2;
                transform.position = pos;
            }
            //Rotate the way the player is facing toward the right
            rb.rotation = Quaternion.Euler(0f, 90f, 0f);
            lastkeypressed = 'd';
        }
    }

    private void LateUpdate()
    {
        //If the player collided with the robot, move it back
        if (collisionscript.hasCollided)
        {
            //The direction you move it back depends on what key was pressed to cause the collision
            switch(lastkeypressed)
            {
                case 'w':
                    var posW = transform.position;
                    posW.z -= 2;
                    transform.position = posW;
                    break;
                case 'a':
                    var posA = transform.position;
                    posA.x += 2;
                    transform.position = posA;
                    break;
                case 's':
                    var posS = transform.position;
                    posS.z += 2;
                    transform.position = posS;
                    break;
                case 'd':
                    var posD = transform.position;
                    posD.x -= 2;
                    transform.position = posD;
                    break;
            }
            //After moving the player back, reset hasCollided
            collisionscript.hasCollided = false;
        }
    }
}