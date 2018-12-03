using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FixItPlayerController : MonoBehaviour
{
    public GameObject playerBody;
    public GameObject bossPanel;
    public GameObject robotPanel;
    public GameObject playerDiedPanel;
    private bool touchPickup;
    private Health HealthScript;

    private void Start()
    {
        touchPickup = false;
        HealthScript = GetComponent<Health>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //For player taking damage from robot
        if (other.tag == "CommonPart" || other.tag == "OutisdePart")
        {
            //Boss robot damages player more than other robots
            if (OverworldGameController.gameInfo.getBossStatus())
                HealthScript.TakeDamage(20);
            else
                HealthScript.TakeDamage(15);
        }
        //For player taking damage from its own weapons
        else if (other.tag == "Oil Spill" || other.tag == "Bomb" || other.tag == "BigBomb")
        {
            HealthScript.TakeDamage(10);
            if(other.tag == "Oil Spill")
            {
                Destroy(other.gameObject);
            }
        }

        //For picking up the big bomb and leaving the fix it world
        else if(other.tag=="BBPickUp" && OverworldGameController.gameInfo.bossFixed)
        {
            if(!touchPickup)
            {
                touchPickup = true;

                //Show a message which says that you have acquired the big bomb and set the flag
                bossPanel.SetActive(true);

                //Destory bomb and load overworld
                Destroy(other.gameObject);
                //code in progress
                if (!OverworldGameController.gameInfo.isSceneLoading())
                    OverworldGameController.gameInfo.changeScene();
                //SceneManager.LoadScene("Overworld");
            }
        }
    }

    private void Update()
    {
        if(HealthScript.Dead)
        {
            //Color the player red to indicate death
            playerBody.transform.GetComponent<Renderer>().material.color = Color.red;

            //Show a message?
            playerDiedPanel.SetActive(true);

            //Return to Overworld
            if (!OverworldGameController.gameInfo.isSceneLoading())
                OverworldGameController.gameInfo.changeScene();
        }
    }

    public void setRobotPanel(bool status)
    {
        robotPanel.SetActive(true);
    }
}
