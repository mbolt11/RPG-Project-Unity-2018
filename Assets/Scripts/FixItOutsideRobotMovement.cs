using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixItOutsideRobotMovement : MonoBehaviour
{

    // Use this for initialization
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
            if(currIndex == 0)
            {
                var pos = transform.position;
                pos.x += 2;
                transform.position = pos;
            }
            else if(currIndex == 1)
            {
                var pos = transform.position;
                pos.z -= 2;
                transform.position = pos;
            }
            else if(currIndex == 2)
            {
                var pos = transform.position;
                pos.x -= 2;
                transform.position = pos;
            }
            else
            {
                var pos = transform.position;
                pos.z += 2;
                transform.position = pos;
            }

            frames = 0;
            movesPerSide++;
        }
    }
}
