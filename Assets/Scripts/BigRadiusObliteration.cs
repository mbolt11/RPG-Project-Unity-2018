using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//YOOO MY FAVORITE PEEPS!
public class BigRadiusObliteration : MonoBehaviour
{

    public float BigBombLifetime;
    public float BombLifetime;
    private float lifetime;
    private bool detonating;
    public ParticleSystem dustParticles;
    public ParticleSystem explosionParticles;

    // Use this for initialization
    void Start()
    {
        Debug.Log(gameObject.name);
        GetComponent<BoxCollider>().enabled = false;
        detonating = false;

        //detonates on touch only & dissapears after an amount of time
        if (tag == "BigBomb")
        {
            lifetime = BigBombLifetime;
            Destroy(gameObject, lifetime);
        }
        //detonates according to a time limit
        else if (tag == "Bomb")
        {
            lifetime = BombLifetime;
            StartCoroutine(TimedExplosion());
        }
    }

    //After a certain amount of time, bomb will explode
    private IEnumerator TimedExplosion()
    {
        //Wait a certain amount of time before the bomb explodes
        yield return new WaitForSeconds(lifetime);

        if (!detonating)
        {
            //Enable the collider for the explosion zone
            GetComponent<BoxCollider>().enabled = true;
            detonating = true;
            if (tag == "Bomb")
                dustParticles.Play();

            explosionParticles.Play();

            //Wait and then destroy the bomb
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }
    }

    private IEnumerator ImmediateExplosion()
    {
        if (!detonating)
        {
            //Enable the collider for the explosion zone
            GetComponent<BoxCollider>().enabled = true;
            detonating = true;
            if (tag == "Bomb")
                dustParticles.Play();

            explosionParticles.Play();

            //Wait and then destroy the bomb
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.tag == "Common Robot" || other.tag == "Outside Robot" || other.tag == "BigBoss" || other.tag == "Player") && !detonating && tag == "BigBomb")
        {
            StartCoroutine(ImmediateExplosion());
        }
    }
}
