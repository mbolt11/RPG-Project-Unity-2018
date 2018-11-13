using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private string currentWeapon;
    public static PlayerWeaponController Instance;

    // Use this for initialization
    void Awake()
    {
        currentWeapon = OverworldGameController.gameInfo.getCurrentWeapon();

        enableCorrectScripts();
    }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void enableCorrectScripts()
    {
        if (currentWeapon == "Hammer" || currentWeapon == "Oil")
        {
            GetComponent<PlayerSwingWeapon>().enabled = true;
            GetComponent<PlayerThrowWeapon>().enabled = false;
        }
        else
        {
            GetComponent<PlayerSwingWeapon>().enabled = false;
            GetComponent<PlayerThrowWeapon>().enabled = true;
        }
    }

    public void changeWeaponType()
    {
        //destroy any lingering exiting weapons
        //deleteOldWeapons();

        //set the new current weapon
        currentWeapon = OverworldGameController.gameInfo.getCurrentWeapon();

        //enable correct scripts
        enableCorrectScripts();
    }

    public void resetSwingWeaponScript()
    {
        //set correct current weapon
        currentWeapon = OverworldGameController.gameInfo.getCurrentWeapon();

        //reset the script to instantiate correct weapon
        GetComponent<PlayerSwingWeapon>().enabled = false;
        GetComponent<PlayerSwingWeapon>().enabled = true;
    }

    public void deleteOldWeapons()
    {
        currentWeapon = OverworldGameController.gameInfo.getCurrentWeapon();
        Destroy(GameObject.FindGameObjectWithTag(currentWeapon));
    }
}
