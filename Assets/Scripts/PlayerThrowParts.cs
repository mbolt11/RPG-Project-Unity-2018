using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowParts : MonoBehaviour {

    public Rigidbody wrench;
    public Transform weaponSpawn;
    public float velocity;

    private string fireButton;
    private bool fired;

	// Use this for initialization
	void Start () {
        fireButton = "Fire1";
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonUp(fireButton) && !fired)
        {
            fired = true;
            Fire();
        }
	}

    private void Fire()
    {
        Rigidbody wrenchInstance = Instantiate(wrench, weaponSpawn.position, weaponSpawn.rotation) as Rigidbody;
        wrenchInstance.velocity = velocity * weaponSpawn.forward;
        fired = false;
    }
}
