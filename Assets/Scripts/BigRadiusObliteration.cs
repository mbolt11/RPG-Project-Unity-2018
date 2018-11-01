using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRadiusObliteration : MonoBehaviour
{

    public float MaxLifeTime;
    public ParticleSystem explosionParticles;

    // Use this for initialization
    void Start()
    {
        Debug.Log(gameObject.name);
        GetComponent<BoxCollider>().enabled = false;
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

    //This is a good idea for the big bomb for future reference
    //After a certain amount of time, bomb will explode
    private IEnumerator Explosion()
    {
        yield return new WaitForSeconds(MaxLifeTime);

        Debug.Log("Exploding");

        //Enable the collider for the explosion zone
        GetComponent<BoxCollider>().enabled = true;
        explosionParticles.Play();

        yield return new WaitForSeconds(1);

        //Destroy the big bomb
        Destroy(gameObject);

    }
}
