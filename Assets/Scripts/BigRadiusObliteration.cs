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
        StartCoroutine(Explosion());
    }

    /* I am trying a different method than this. The bomb will "explode" after a certain amount of time

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
    */

    //After a certain amount of time, bomb will explode
    private IEnumerator Explosion()
    {
        yield return new WaitForSeconds(MaxLifeTime);

        //Enable the collider for the explosion zone
        GetComponent<BoxCollider>().enabled = true;

        //Put a particle effect here

        //Destroy the big bomb
        Destroy(gameObject);

    }
}
