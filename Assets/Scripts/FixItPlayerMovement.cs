using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixItPlayerMovement : MonoBehaviour 
{
    //Set player target start position
    private void Start()
    {
        Vector3 playerstartpos = new Vector3(-8, 0, -6);
        transform.position = playerstartpos;
    }

    // Move the player target by increments of 2 on button up
    void Update () 
    {
        if(Input.GetKeyUp("w"))
        {
            //Move the player target only if it won't go past the edge
            if(transform.position.z < 8)
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
    }
}
