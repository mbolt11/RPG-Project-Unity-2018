using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowWeapon : MonoBehaviour {

    public Transform wrenchPos;
    public Transform bombPos;
    public Transform bigBombPos;
    private float wrenchVelocity = 20;
    private float bombVelocity = 10;

    private Rigidbody wrench;
    private Rigidbody bomb;
    private Transform bigBomb;

    private string selectedWeapon;
    private Transform weaponSpawn;
    private Rigidbody weaponInstance;

    public GameObject playerArm;
    private Transform armtransform;

    private string fireButton;
    private bool fired;

    private Vector3 currentarmpos;
    private Quaternion currentarmrot;

	// Use this for initialization
	void OnEnable ()
    {
        fireButton = "Fire1";
        selectedWeapon = OverworldGameController.gameInfo.getCurrentWeapon();

        wrench = OverworldGameController.gameInfo.Wrench.GetComponent<Rigidbody>();
        bomb = OverworldGameController.gameInfo.Bomb.GetComponent<Rigidbody>();
        bigBomb = OverworldGameController.gameInfo.BigBomb.GetComponent<Transform>();

        //Get the transform of the player's arm for later
        armtransform = playerArm.GetComponent<Transform>();

        //select correct transform for the particular weapon
        changeTransform();
    }
	
	// Update is called once per frame
	void Update () 
    {
        selectedWeapon = OverworldGameController.gameInfo.getCurrentWeapon();

        if (Input.GetButtonUp(fireButton) && !fired)
        {
            //Fire the wrench
            fired = true;
            Fire();
        }
    }

    public void changeWeapon(string newWeapon)
    {
        //delete old weapons
        PlayerWeaponController.Instance.deleteOldWeapons();

        //set to new weapon
        selectedWeapon = newWeapon;
        changeTransform();
    }

    private void Fire()
    {
        //Move the player's arm to show him throwing
        Vector3 throwPos = new Vector3(armtransform.localPosition.x, armtransform.localPosition.y, armtransform.localPosition.z + 0.2f);
        Quaternion throwRot = Quaternion.Euler(armtransform.localRotation.x + 90, armtransform.localRotation.y, armtransform.localRotation.z);
        armtransform.localPosition = throwPos;
        armtransform.localRotation = throwRot;

        //Create Weapon
        switch (selectedWeapon)
        {
            case "bomb":
                Rigidbody weaponInstance = Instantiate(bomb, weaponSpawn.position, weaponSpawn.rotation) as Rigidbody;
                weaponInstance.velocity = bombVelocity * weaponSpawn.forward;
                break;
            case "bigBomb":
                Transform weaponInstance2 = Instantiate(bigBomb, weaponSpawn.position, weaponSpawn.rotation);
                break;
            default:
                Rigidbody weaponInstance3 = Instantiate(wrench, weaponSpawn.position, weaponSpawn.rotation) as Rigidbody;
                weaponInstance3.velocity = wrenchVelocity * weaponSpawn.forward;
                break;
        }

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

    //select correct transform for the new current weapon
    private void changeTransform()
    {
        switch (selectedWeapon)
        {
            case "bomb":
                weaponSpawn = bombPos;
                break;
            case "bigBomb":
                weaponSpawn = bigBombPos;
                break;
            default:
                weaponSpawn = wrenchPos;
                break;
        }
    }
}
