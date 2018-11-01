using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRadiusObliteration : MonoBehaviour
{

    public float MaxLifeTime;
    //public ParticleSystem bombParticles;
    //public ParticleSystem bigBombPartices;

    // Use this for initialization
    void Start()
    {
        Debug.Log(gameObject.name);
        Destroy(gameObject, MaxLifeTime);
    }

    //need to collect all colliders within radius of zone collider as well
    //first thing the normal collider touches causes a blast
    private void OnTriggerEnter(Collider other)
    {
        //robot or player could get hit
        if(other.tag == "Player")
        {
            //player health reduced by 50 for big bomb
            //30 for bomb

        }
        else if(other.tag == "Robot")
        {
            //robot health reduced by 50 for big bomb 
            //30 for bomb
        }

        //Destroy.
    }
}
