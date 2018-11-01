using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixItGameController : MonoBehaviour
{
    private int weaponCounter;

	// Use this for initialization
	void Start ()
    {
        weaponCounter = 0;	
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Return))
        {
            string weapon = OverworldGameController.gameInfo.selectedTools[weaponCounter].name;

            OverworldGameController.gameInfo.setCurrentWeapon(weapon);
            Debug.Log(OverworldGameController.gameInfo.getCurrentWeapon());

            if (weaponCounter < OverworldGameController.gameInfo.selectedTools.Count - 1)
            {
                weaponCounter++;
            }
            else
            {
                weaponCounter = 0;
            }

            //Change Weapon Player is Using (Uses Function in PlayerWeaponController Script
            GameObject.Find("Fix-It Player").GetComponent<PlayerWeaponController>().changeWeaponType();
        }
	}
}
