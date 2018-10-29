using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotWeaponObliteration : MonoBehaviour
{

    public float MaxLifeTime = 4f;
    public ParticleSystem ObliterationParticles;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, MaxLifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if the robot is hit then special noise, particles, and event occurs??
        if (other.CompareTag("Player"))
        {
            //Effect when hitting the player
            ObliterationParticles.Play();
            //Debug.Log("obliterate!");
        }
        //Debug.Log(other.tag);
        ObliterationParticles.transform.parent = null;

        Destroy(ObliterationParticles.gameObject, ObliterationParticles.main.duration);
        Destroy(gameObject);
    }
}