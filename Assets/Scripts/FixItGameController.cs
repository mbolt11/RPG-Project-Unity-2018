using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixItGameController : MonoBehaviour
{
    private int weaponCounter;
    private string weapon;

	// Use this for initialization
	void Start ()
    {
        weaponCounter = 0;
        weapon = OverworldGameController.gameInfo.getCurrentWeapon();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Return))
        {
            if (weaponCounter < OverworldGameController.gameInfo.selectedTools.Count - 1)
            {
                weaponCounter++;
            }
            else
            {
                weaponCounter = 0;
            }

            string nextWeapon = OverworldGameController.gameInfo.selectedTools[weaponCounter].name;

            //destroy old swing weapon
            if(weapon == "Oil" || weapon == "Hammer")
                PlayerWeaponController.Instance.deleteOldWeapons();

            OverworldGameController.gameInfo.setCurrentWeapon(nextWeapon);
            Debug.Log("changed weapon to " + OverworldGameController.gameInfo.getCurrentWeapon());

            //Change Weapon Player is Using (Uses Function in PlayerWeaponController Script
            if(((weapon == "Oil" || weapon == "Hammer") && (nextWeapon != "Oil" && nextWeapon != "Hammer")) || ((nextWeapon == "Oil" || nextWeapon == "Hammer") && (weapon != "Oil" && weapon != "Hammer")))
                GameObject.Find("Fix-It Player").GetComponent<PlayerWeaponController>().changeWeaponType();
            else if((weapon == "Oil" || weapon == "Hammer") && (nextWeapon == "Oil" || nextWeapon == "Hammer"))
                GameObject.Find("Fix-It Player").GetComponent<PlayerWeaponController>().resetSwingWeaponScript();
            else
                GameObject.Find("Fix-It Player").GetComponent<PlayerThrowWeapon>().changeWeapon(nextWeapon);

            weapon = nextWeapon;
        }
	}
}
