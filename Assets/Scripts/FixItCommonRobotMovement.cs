using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixItCommonRobotMovement : MonoBehaviour {

    public GameObject playerbody;
    public GameObject playerTarget;
    private Renderer pbrenderer;
    private Transform pttransform;

    private bool panleft;
    private int frames;

	void Start () 
    {
        Vector3 robotstartpos = new Vector3(0, -1, 8);
        transform.position = robotstartpos;
        panleft = true;
        frames = 60;

        pttransform = playerTarget.GetComponent<Transform>();
        pbrenderer = playerbody.GetComponent<Renderer>();
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
                    //Hold the values for where the robot will be once moved and the current player position
                    Vector3 futurerobotpos = new Vector3(transform.position.x + 2, transform.position.y + 1, transform.position.z);
                    Vector3 playerposition = new Vector3(pttransform.position.x, pttransform.position.y, pttransform.position.z);

                    //If the player is not in the way, move the robot
                    if(futurerobotpos != playerposition)
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

                    //If robot has reached the edge, switch directions
                    if (transform.position.x >= 6)
                    {
                        panleft = false;
                    }
                }
            }
            else
            {
                //move right one square
                if (transform.position.x > -8)
                {
                    //Hold the values for where the robot will be once moved and the current player position
                    Vector3 futurerobotpos = new Vector3(transform.position.x - 2, transform.position.y + 1, transform.position.z);
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

                    //If robot has reached the edge, switch directions
                    if (transform.position.x <= -8)
                    {
                        panleft = true;
                    }
                }
            }
            frames = 0;
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
