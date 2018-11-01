using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwingWeapon : MonoBehaviour {

    private GameObject hammer;
    private GameObject oil;
    public Transform hammerSpawn;
    public Transform oilSpawn;
    public GameObject oilSpill;
    //public float velocity;

    private string selectedWeapon;
    private GameObject weaponInstance;

    public GameObject playerArm;
    private Transform armtransform;

    private string fireButton;
    private bool swinging;

    private Vector3 currentarmpos;
    private Quaternion currentarmrot;

    // Use this for initialization
    void OnEnable()
    {
        fireButton = "Fire1";
        selectedWeapon = OverworldGameController.gameInfo.getCurrentWeapon();

        //Get the transform of the player's arm for later
        armtransform = playerArm.GetComponent<Transform>();

        //create the hammer
        //probrably will want to make hammer a child of the arm
        hammer = OverworldGameController.gameInfo.Hammer;
        oil = OverworldGameController.gameInfo.Oil;

        if (selectedWeapon == "Hammer")
        {
            weaponInstance = Instantiate(hammer, hammerSpawn.position, hammerSpawn.rotation);
            Debug.Log("hammer made");
        }
        else if (selectedWeapon == "Oil")
            weaponInstance = Instantiate(oil, oilSpawn.position, oilSpawn.rotation);
        else
            Debug.Log("script enabled when not hammer or oil? " + selectedWeapon);

        //Vector3 scaledOil = weaponInstance.transform.localScale;
        if (weaponInstance != null)
            weaponInstance.transform.parent = armtransform;
        else
            Debug.Log("Swingweapon error");

        //if(selectedWeapon == "oil")
            //weaponInstance.transform.localScale = scaledOil;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp(fireButton) && !swinging)
        {
            //Fire the wrench
            swinging = true;
            Fire();
        }
    }

    private void Fire()
    {
        //Move the player's arm to show him throwing
        Vector3 throwPos = new Vector3(armtransform.localPosition.x, armtransform.localPosition.y, armtransform.localPosition.z + 0.2f);
        Quaternion throwRot = Quaternion.Euler(armtransform.localRotation.x + 90, armtransform.localRotation.y, armtransform.localRotation.z);
        armtransform.localPosition = throwPos;
        armtransform.localRotation = throwRot;
            
        //moving the arm should swing the hammer (and oilcan?)
        swinging = false;

        //Make the oilcan spill
        if(selectedWeapon == "Oil")
        {
            OilSpill();
        }

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

    //For the oil spill functionality
    void OilSpill()
    {
        Vector3 spillLocation;

        //The player is facing North
        if(transform.forward.z > .5)
        {
            spillLocation = new Vector3(transform.position.x, 0.1f, transform.position.z + 2);
        }
        //The player is facing South
        else if(transform.forward.z < -.5)
        {
            spillLocation = new Vector3(transform.position.x, 0.1f, transform.position.z - 2);
        }
        //The player is facing East
        else if(transform.forward.x > .5)
        {
            spillLocation = new Vector3(transform.position.x + 2, 0.1f, transform.position.z);
        }
        //The player is facing West
        else
        {
            spillLocation = new Vector3(transform.position.x - 2, 0.1f, transform.position.z);
        }

        if(spillLocation.x >= -8 && spillLocation.x <= 6)
        {
            if(spillLocation.z >= -6 && spillLocation.z <= 8)
            {
                Instantiate(oilSpill, spillLocation, Quaternion.identity);
            }
        }

    }
}
