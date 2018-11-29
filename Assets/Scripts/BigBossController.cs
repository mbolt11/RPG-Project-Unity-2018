using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BigBossController : MonoBehaviour
{
    public GameObject robotBody;
    public ParticleSystem explosionParticles;

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
            transform.Rotate(0, 180, 0);
            //transform.position = new Vector3(transform.position.x, -.75f, transform.position.z);
        }

        firstDeath = false;
        HealthScript = GetComponentInParent<Health>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //When the robot gets hit by a tool/weapon
        if (other.tag == "Wrench" || other.tag == "Hammer" || other.tag == "Oil Spill" || other.tag == "Bomb" || other.tag == "BigBomb")
        {
            //If this is a bomb or wrench, play the explosion and destroy it
            if (other.tag == "Bomb" || other.tag == "BigBomb" || other.tag == "Wrench")
            {
                explosionParticles.Play();
                Destroy(other.gameObject);
            }

            //Take damage
            HealthScript.TakeDamage(5);

            //When health reaches 0, the robot is dead
            if (HealthScript.Dead && !firstDeath)
            {
                firstDeath = true;
                //Change robot color to green to indicate fixed
                robotBody.transform.GetComponent<Renderer>().material.color = Color.green;

                //Stop throwing parts
                GetComponent<RobotThrowsParts>().enabled = false;

                //Add this robot to the list of robots that have been defeated
                OverworldGameController.gameInfo.RobotDefeated();

                if (!OverworldGameController.gameInfo.isSceneLoading())
                {
                    GameObject.FindGameObjectWithTag("GamePieces").transform.GetChild(0).gameObject.GetComponent<FixItPlayerController>().setRobotPanel(true);
                    //code in progress
                    OverworldGameController.gameInfo.changeScene();
                }
            }
        }
    }
}

  