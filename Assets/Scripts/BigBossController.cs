using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BigBossController : MonoBehaviour
{
    public GameObject robotBody;

    private Health HealthScript;
    private bool firstDeath;


    // Use this for initialization
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Overworld")
        {
            if (OverworldGameController.gameInfo.bossAlive)
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);
            GetComponent<RobotThrowsParts>().enabled = false;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            //transform.Rotate(0, 180, 0);
            //transform.position = new Vector3(transform.position.x, -.75f, transform.position.z);
        }

        firstDeath = false;
        HealthScript = GetComponentInParent<Health>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //When the robot gets hit by a tool/weapon
        if (other.tag == "Wrench")
        {
            //Play the effect and destroy the wrench
            other.GetComponentInChildren<ParticleSystem>().Play();
            StartCoroutine(DestroyWrench(other.gameObject));

            //Update health
            HealthScript.TakeDamage(1);

            //Check if the robot's health is at 0 = dead
            if (HealthScript.Dead && !firstDeath)
            {
                IsDead();
            }
        }
        else if (other.tag == "Hammer")
        {
            //Update health
            HealthScript.TakeDamage(5);

            //Check if the robot's health is at 0 = dead
            if (HealthScript.Dead && !firstDeath)
            {
                IsDead();
            }
        }
        else if (other.tag == "Oil Spill")
        {
            //Update health
            HealthScript.TakeDamage(8);

            //Check if the robot's health is at 0 = dead
            if (HealthScript.Dead && !firstDeath)
            {
                IsDead();
            }
        }
        else if (other.tag == "Bomb")
        {
            if (other.transform.GetChild(4).GetComponent<ParticleSystem>().isPlaying)
            {
                //Update health
                HealthScript.TakeDamage(10);

                //Check if the robot's health is at 0 = dead
                if (HealthScript.Dead && !firstDeath)
                {
                    IsDead();
                }
            }
        }
        else if (other.tag == "BigBomb")
        {
            if (other.transform.GetChild(4).GetComponent<ParticleSystem>().isPlaying)
            {
                //Update Health
                HealthScript.TakeDamage(20);

                //Check if the robot's health is at 0 = dead
                if (HealthScript.Dead && !firstDeath)
                {
                    IsDead();
                }
            }
        }
    }

    void IsDead()
    {
        firstDeath = true;
        //Change robot color to green to indicate fixed
        robotBody.transform.GetComponent<Renderer>().material.color = Color.green;

        //Stop throwing parts
        GetComponent<RobotThrowsParts>().enabled = false;

        //Add this robot to the list of robots that have been defeated
        OverworldGameController.gameInfo.RobotDefeated();
        OverworldGameController.gameInfo.gameOver = true;

        if (!OverworldGameController.gameInfo.isSceneLoading())
        {
            GameObject.FindGameObjectWithTag("GamePieces").transform.GetChild(0).gameObject.GetComponent<FixItPlayerController>().setRobotPanel(true);
            //code in progress
            OverworldGameController.gameInfo.changeScene();
        }
    }

    private IEnumerator DestroyWrench(GameObject wrench)
    {
        yield return new WaitForSeconds(.25f);
        Destroy(wrench);
    }
}

  