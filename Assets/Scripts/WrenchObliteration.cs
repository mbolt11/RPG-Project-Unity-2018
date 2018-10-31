using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchObliteration : MonoBehaviour {

    public float MaxLifeTime = 2f;
    public ParticleSystem ObliterationParticles;
    
	// Use this for initialization
	void Start () 
    {
        Debug.Log(gameObject.name);
        if (gameObject.name != "hammer(Clone)")
        {
            Destroy(gameObject, MaxLifeTime);
        }
        else
        {
            Debug.Log("wont be destroyed");
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        //if the robot is hit then special noise, particles, and event occurs??
        if(other.CompareTag("Robot"))
        {
            //Effect when hitting the robot
            ObliterationParticles.Play();

            ObliterationParticles.transform.parent = null;

            Destroy(ObliterationParticles.gameObject, ObliterationParticles.main.duration);
            Destroy(gameObject);
        }

        //ObliterationParticles.transform.parent = null;

        //Destroy(ObliterationParticles.gameObject, ObliterationParticles.main.duration);
        //Destroy(gameObject);
    }
}
