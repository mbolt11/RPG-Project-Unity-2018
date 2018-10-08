using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowParts : MonoBehaviour {

    public Rigidbody wrench;
    public Transform weaponSpawn;
    public float velocity;

    public GameObject playerArm;
    public GameObject playerBody;
    private Transform armtransform;
    private Transform bodytransform;

    private string fireButton;
    private bool fired;

    private Vector3 currentarmpos;
    private Quaternion currentarmrot;

	// Use this for initialization
	void Start ()
    {
        fireButton = "Fire1";

        //Get the transform of the player's arm and body for later
        armtransform = playerArm.GetComponent<Transform>();
        bodytransform = playerBody.GetComponent<Transform>();
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
        Vector3 armposition1 = new Vector3(bodytransform.position.x + 0.7f, bodytransform.position.y + 0.5f, bodytransform.position.z);
        Quaternion armrotation1 = Quaternion.Euler(bodytransform.rotation.x + 270, bodytransform.rotation.y, bodytransform.rotation.z - 150);
        armtransform.SetPositionAndRotation(armposition1, armrotation1);

        //Throw the wrench
        Rigidbody wrenchInstance = Instantiate(wrench, weaponSpawn.position, weaponSpawn.rotation) as Rigidbody;
        wrenchInstance.velocity = velocity * weaponSpawn.forward;
        fired = false;

        //ResetArm the arm position after throwing
        StartCoroutine("ResetArm");
    }

    private IEnumerator ResetArm()
    {
        yield return new WaitForSeconds(0.3f);

        //Move the player's arm back to poised position
        Vector3 armposition2 = new Vector3(bodytransform.position.x + 0.7f, bodytransform.position.y + 0.5f, bodytransform.position.z);
        Quaternion armrotation2 = Quaternion.Euler(bodytransform.rotation.x + 180, bodytransform.rotation.y, bodytransform.rotation.z - 150);
        armtransform.SetPositionAndRotation(armposition2, armrotation2);
    }
}
