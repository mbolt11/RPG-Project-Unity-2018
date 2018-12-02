using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoRadiusObliteration : MonoBehaviour {

    public float MaxLifeTime;
    public ParticleSystem ObliterationParticles;
    
	// Use this for initialization
	void Start () 
    {
        //Debug.Log(gameObject.name);
        Destroy(gameObject, MaxLifeTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        //if the robot is hit then special noise, particles, and event occurs??
        if (gameObject.tag == "Wrench")
        {
            if (other.CompareTag("Robot"))
            {
                //Effect when hitting the robot
                ObliterationParticles.Play();

                ObliterationParticles.transform.parent = null;

                Destroy(ObliterationParticles.gameObject, ObliterationParticles.main.duration);
                Destroy(gameObject);
            }
        }
        else
        {
            ObliterationParticles.Play();
        }
    }
}
