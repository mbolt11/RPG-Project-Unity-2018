using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private OverworldGameController gameInfo;
    private string currentWeapon;

    // Use this for initialization
    void Start()
    {

        if (GameObject.Find("GameController") != false)
        {
            gameInfo = GameObject.Find("GameController").GetComponent<OverworldGameController>().getSingleton();
            currentWeapon = gameInfo.getCurrentWeapon();
        }
        else
        {
            currentWeapon = "wrench";
        }

        if (currentWeapon == "hammer" || currentWeapon == "oil")
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
}
