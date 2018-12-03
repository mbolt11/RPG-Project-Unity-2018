using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//YOOO MY FAVORITE PEEPS!
public class BigRadiusObliteration : MonoBehaviour
{

    public float BigBombLifetime;
    public float BombLifetime;
    private float lifetime;
    public ParticleSystem explosionParticles;

    // Use this for initialization
    void Start()
    {
        Debug.Log(gameObject.name);
        GetComponent<BoxCollider>().enabled = false;

        if (tag == "BigBomb")
        {
            lifetime = BigBombLifetime;
        }
        else if (tag == "Bomb")
        {
            lifetime = BombLifetime;
        }

        StartCoroutine(Explosion());
    }

    //This is a good idea for the big bomb for future reference
    //After a certain amount of time, bomb will explode
    private IEnumerator Explosion()
    {
        //Wait a certain amount of time before the bomb explodes
        yield return new WaitForSeconds(lifetime);

        //Enable the collider for the explosion zone
        GetComponent<BoxCollider>().enabled = true;
        explosionParticles.Play();

        //Wait and then destroy the bomb
        yield return new WaitForSeconds(1);
        Destroy(gameObject);

    }
}
