using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowWeapon : MonoBehaviour {

    public Transform weaponSpawn;
    public float velocity;

    private Rigidbody wrench;
    private Rigidbody bomb;
    private Rigidbody bigBomb;

    private string selectedWeapon;
    private Rigidbody weaponInstance;

    public GameObject playerArm;
    private Transform armtransform;

    private string fireButton;
    private bool fired;

    private Vector3 currentarmpos;
    private Quaternion currentarmrot;

	// Use this for initialization
	void Start ()
    {
        fireButton = "Fire1";
        selectedWeapon = "wrench";

        wrench = OverworldGameController.gameInfo.wrench.GetComponent<Rigidbody>();
        bomb = OverworldGameController.gameInfo.bomb.GetComponent<Rigidbody>();
        bigBomb = OverworldGameController.gameInfo.bigBomb.GetComponent<Rigidbody>();

        //Get the transform of the player's arm for later
        armtransform = playerArm.GetComponent<Transform>();
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

    public void changeWeapon(string newWeapon)
    {
        selectedWeapon = newWeapon;
    }

    private void Fire()
    {
        //Move the player's arm to show him throwing
        Vector3 throwPos = new Vector3(armtransform.localPosition.x, armtransform.localPosition.y, armtransform.localPosition.z + 0.2f);
        Quaternion throwRot = Quaternion.Euler(armtransform.localRotation.x + 90, armtransform.localRotation.y, armtransform.localRotation.z);
        armtransform.localPosition = throwPos;
        armtransform.localRotation = throwRot;

        //Create Weapon
        Rigidbody weaponInstance;
        switch (selectedWeapon)
        {
            case "bomb":
                weaponInstance = Instantiate(bomb, weaponSpawn.position, weaponSpawn.rotation) as Rigidbody;
                break;
            case "bigBomb":
                weaponInstance = Instantiate(bigBomb, weaponSpawn.position, weaponSpawn.rotation) as Rigidbody;
                break;
            default:
                weaponInstance = Instantiate(wrench, weaponSpawn.position, weaponSpawn.rotation) as Rigidbody;
                break;
        }

        //throw the weapon
        weaponInstance.velocity = velocity * weaponSpawn.forward;
        fired = false;

        //ResetArm the arm position after throwing
        StartCoroutine("ResetArm");
    }

    private IEnumerator ResetArm()
    {
        yield return new WaitForSeconds(0.3f);

        //Move the player's arm back to ready position
        Vector3 readyPos = new Vector3(armtransform.localPosition.x, armtransform.localPosition.y, armtransform.localPosition.z - 0.2f);
        Quaternion readyRot = Quaternion.Euler(armtransform.localRotation.x - 180, armtransform.localRotation.y, armtransform.localRotation.z - 150);
        armtransform.localPosition = readyPos;
        armtransform.localRotation = readyRot;
    }
}
