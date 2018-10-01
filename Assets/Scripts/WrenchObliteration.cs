using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchObliteration : MonoBehaviour {

    public float MaxLifeTime = 4f;
    public ParticleSystem ObliterationParticles;
    
	// Use this for initialization
	void Start () {
        Destroy(gameObject, MaxLifeTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        //if the robot is hit then special noise, particles, and event occurs??
        if(other.CompareTag("Robot"))
        {
            //damage to the robot
        }

        //ObliterationParticles.transform.parent = null;
        ObliterationParticles.Play();

        Destroy(ObliterationParticles, ObliterationParticles.main.duration);
        Destroy(gameObject);
    }
}
