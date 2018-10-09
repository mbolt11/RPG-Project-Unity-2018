using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixItPlayerMovement : MonoBehaviour 
{
    // Move the player target by increments of 2 on button up

    public GameObject player;

    private void Start()
    {
        //Set player target start position
        Vector3 playerstartpos = new Vector3(-8, 0, -6);
        transform.position = playerstartpos;

        //Set player start rotation
        player.transform.Rotate(0, 0, 0);
    }

    // Move the player target by increments of 2 on button up
    void Update () 
    {
        if(Input.GetKeyUp("w"))
        {
            //Move the player target only if it won't go past the edge
            if (transform.position.z < 8)
            {
                var pos = transform.position;
                pos.z += 2;
                transform.position = pos;
            }
        }

        if(Input.GetKeyUp("a"))
        {
            //Move the player target only if it won't go past the edge
            if (transform.position.x > -8)
            {
                var pos = transform.position;
                pos.x -= 2;
                transform.position = pos;
            }
            //Rotate the way the player is facing toward the left
            player.transform.Rotate(0, -90, 0);
        }

        if (Input.GetKeyUp("s"))
        {
            //Move the player target only if it won't go past the edge
            if(transform.position.z > -6)
            {
                var pos = transform.position;
                pos.z -= 2;
                transform.position = pos;
            }
        }

        if (Input.GetKeyUp("d"))
        {
            //Move the player target only if it won't go past the edge
            if (transform.position.x < 6)
            {
                var pos = transform.position;
                pos.x += 2;
                transform.position = pos;
            }
        }
            //Rotate the way the player is facing toward the right
            player.transform.Rotate(0,90,0);
        }
    }

