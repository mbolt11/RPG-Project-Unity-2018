﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixItOutsideRobotMovement : MonoBehaviour
{

    public GameObject playerbody;
    public GameObject playerTarget;
    private Renderer pbrenderer;
    private Transform pttransform;

    private int frames;
    private int movesPerSide;
    private int currIndex;
    private Transform currRobot;


    void Start()
    {
        Vector3 robotstartpos = new Vector3(-8, 0, 8);
        transform.position = robotstartpos;
        currIndex = 0;
        movesPerSide = 0;
        frames = 60;
        currRobot = GameObject.Find("Fix-It Robot").GetComponentInChildren<Transform>();

        pttransform = playerTarget.GetComponent<Transform>();
        pbrenderer = playerbody.GetComponent<Renderer>();

        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        frames++;

        if(movesPerSide > 6)
        {
            //rotate 90 degrees
            currRobot.Rotate(0, 90f, 0);
            //currRobot.rotation = Quaternion.Euler(0f, currRobot.rotation.y + 45f, 0f);
            Debug.Log("TagRotation:" + currRobot.tag);
            //Debug.Log("movesPerSide:" +movesPerSide);
            movesPerSide = 0;

            currIndex++;
            if (currIndex > 3)
                currIndex = 0;   
        }

        if (frames >= 60)
        {
            if (currIndex == 0)
            {
                //Hold the values for where the robot will be once moved and the current player position
                Vector3 futurerobotpos = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
                Vector3 playerposition = new Vector3(pttransform.position.x, pttransform.position.y, pttransform.position.z);

                //If the player is not in the way, move the robot
                if (futurerobotpos != playerposition)
                {
                    var pos = transform.position;
                    pos.x += 2;
                    transform.position = pos;
                }
                //If the player is in the way, it takes damage
                else
                {
                    PlayerTakeDamage(pbrenderer);
                }

            }
            else if(currIndex == 1)
            {
                //Hold the values for where the robot will be once moved and the current player position
                Vector3 futurerobotpos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);
                Vector3 playerposition = new Vector3(pttransform.position.x, pttransform.position.y, pttransform.position.z);

                //If the player is not in the way, move the robot
                if (futurerobotpos != playerposition)
                {
                    var pos = transform.position;
                    pos.z -= 2;
                    transform.position = pos;
                }
                //If the player is in the way, it takes damage
                else
                {
                    PlayerTakeDamage(pbrenderer);
                }
            }
            else if(currIndex == 2)
            {
                //Hold the values for where the robot will be once moved and the current player position
                Vector3 futurerobotpos = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z);
                Vector3 playerposition = new Vector3(pttransform.position.x, pttransform.position.y, pttransform.position.z);

                //If the player is not in the way, move the robot
                if (futurerobotpos != playerposition)
                {
                    var pos = transform.position;
                    pos.x -= 2;
                    transform.position = pos;
                }
                //If the player is in the way, it takes damage
                else
                {
                    PlayerTakeDamage(pbrenderer);
                }
            }
            else
            {
                //Hold the values for where the robot will be once moved and the current player position
                Vector3 futurerobotpos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);
                Vector3 playerposition = new Vector3(pttransform.position.x, pttransform.position.y, pttransform.position.z);

                //If the player is not in the way, move the robot
                if (futurerobotpos != playerposition)
                {
                    var pos = transform.position;
                    pos.z += 2;
                    transform.position = pos;
                }
                //If the player is in the way, it takes damage
                else
                {
                    PlayerTakeDamage(pbrenderer);
                }
            }

            frames = 0;
            movesPerSide++;
        }
    }

    void PlayerTakeDamage(Renderer playerbodyrenderer)
    {
        //Change the player's body color to show damage
        float newR = playerbodyrenderer.material.color.r + 0.25f;
        if (newR > 1)
        {
            newR = 1;
        }
        float newG = playerbodyrenderer.material.color.g - 0.13f;
        if (newG < 0)
        {
            newG = 0;
        }
        float newB = playerbodyrenderer.material.color.b - 0.2f;
        if (newB < 0)
        {
            newB = 0;
        }
        Color change = new Color(newR, newG, newB, 1);
        playerbodyrenderer.material.color = change;
    }
}
