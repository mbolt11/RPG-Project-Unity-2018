using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowParts : MonoBehaviour {

    public Rigidbody wrench;
    public Transform weaponSpawn;
    public float velocity;

    public GameObject playerArm;
    private Transform armtransform;

    private string fireButton;
    private bool fired;

    private Vector3 normalarmpos;
    private Quaternion normalarmrot;

	// Use this for initialization
	void Start ()
    {
        fireButton = "Fire1";

        //Get the transform of the player's arm for later
        armtransform = playerArm.GetComponent<Transform>();

        //Save the starting arm position
        normalarmpos = armtransform.position;
        normalarmrot = armtransform.rotation;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetButtonUp(fireButton) && !fired)
        {
            //Fire the wrench
            fired = true;
            Fire();
        }
	}

    private void Fire()
    {
        //Move the player's arm to show him throwing
        Vector3 armposition1 = new Vector3(armtransform.position.x, armtransform.position.y, armtransform.position.z + 0.5f);
        Quaternion armrotation1 = Quaternion.Euler(armtransform.rotation.x + 90, armtransform.rotation.y, armtransform.rotation.z);
        armtransform.SetPositionAndRotation(armposition1, armrotation1);

        //Throw the wrench
        Rigidbody wrenchInstance = Instantiate(wrench, weaponSpawn.position, weaponSpawn.rotation) as Rigidbody;
        wrenchInstance.velocity = velocity * weaponSpawn.forward;
        StartCoroutine("ResetArm");
        fired = false;
    }

    private IEnumerator ResetArm()
    {
        yield return new WaitForSeconds(.1f);

        //Move the player's arm back to starting position
        armtransform.SetPositionAndRotation(normalarmpos, normalarmrot);
    }
}
