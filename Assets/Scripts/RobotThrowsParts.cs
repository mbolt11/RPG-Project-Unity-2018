using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotThrowsParts : MonoBehaviour
{

    public Rigidbody cog;
    public Transform weaponSpawn;
    public float velocity;
    private float secondsBetweenThrow;

    private float framestoThrow;
    private int frames;

    // Use this for initialization
    void Start()
    {
        if(tag == "BigBoss")
        {
            secondsBetweenThrow = 0.5f;
        }
        else
        {
            secondsBetweenThrow = 2f;
        }
        framestoThrow = 60 * secondsBetweenThrow;
        frames = 0;
    }

    // Update is called once per frame
    void Update()
    {
        frames++;

        if (frames >= framestoThrow)
        {
            //Fire the parts
            Fire();
            frames = 0;
        }
    }

    private void Fire()
    {
        Rigidbody cogInstance = Instantiate(cog, weaponSpawn.position, weaponSpawn.rotation) as Rigidbody;
        cogInstance.velocity = velocity * weaponSpawn.forward;
    }
}
