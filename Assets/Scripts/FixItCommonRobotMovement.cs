using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixItCommonRobotMovement : MonoBehaviour {

    // Use this for initialization
    private bool panleft;
    private bool panright;
    private int frames;

	void Start () {
        Vector3 robotstartpos = new Vector3(0, 0, 8);
        transform.position = robotstartpos;
        panleft = true;
        panright = false;
        frames = 60;
    }
	
	// Update is called once per frame
	void Update () {

        frames++;

        if (frames >= 60)
        {
            if (panleft)
            {
                //move left one square
                if (transform.position.x < 6)
                {
                    var pos = transform.position;
                    pos.x += 2;
                    transform.position = pos;

                    if (transform.position.x >= 6)
                    {
                        panleft = false;
                        panright = true;
                    }
                }
            }
            else
            {
                //move right one square
                if (transform.position.x > -8)
                {
                    var pos = transform.position;
                    pos.x -= 2;
                    transform.position = pos;

                    if (transform.position.x <= -8)
                    {
                        panleft = true;
                        panright = false;
                    }
                }
            }
            frames = 0;
        }
    }
}
