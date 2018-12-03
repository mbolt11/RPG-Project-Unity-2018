using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RobotController : MonoBehaviour
{
    public GameObject robotBody;
    public GameObject BBPickUp;
    public ParticleSystem explosionParticles;

    [HideInInspector]
    public bool isBoss;

    private Health HealthScript;
    private bool firstDeath;

    // Use this for initialization
    void Awake()
    {
        firstDeath = false;

        //Various setup things
        Scene currentScene = SceneManager.GetActiveScene();

        //If we are in the overworld
        if(currentScene.name == "Overworld")
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            //Enable/disable appropriate scripts
            GetComponent<RobotThrowsParts>().enabled = false;
            //GetComponent<CommonRobotOverworldMovement>().enabled = true;

            //This randomly assigns bosses
            int rand = Random.Range(0, 4);
            isBoss = rand == 0 ? true : false;
        }
        //If we are in the Fix It World
        else
        {
            //Enable/disable appropriate scripts
            GetComponent<RobotThrowsParts>().enabled = true;
            GetComponent<OverworldRobotMovement>().enabled = false;
            transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        }

        //Color robot if it's a boss
        //Health or movement should be different in the future
        if (tag == "Common Robot")
        {
            robotBody.transform.GetComponent<Renderer>().material.color = new Color(1f, .033f, .0141f, 1f);
        }
        else if (tag == "Outside Robot")
        {
            robotBody.transform.GetComponent<Renderer>().material.color = new Color(0.318f, 0.585f, .0118f, 1f);
        }
        if (isBoss)
        {
            //Debug.Log("Should be yellow");
            robotBody.transform.GetComponent<Renderer>().material.color = new Color(.83f, .69f, .22f, 1f);
        }
    }

    private void Start()
    {
        HealthScript = GetComponentInParent<Health>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //When the robot gets hit by a tool/weapon
        if (other.tag == "Wrench")
        {
            //Play the explosion and destroy weapon
            Debug.Log("Explosion should play");
            if (!explosionParticles.isPlaying)
                explosionParticles.Play();
            Destroy(other.gameObject);

            if (OverworldGameController.gameInfo.getBossStatus())
            {
                HealthScript.TakeDamage(5);
            }
            else
            {
                HealthScript.TakeDamage(8);
            }

            //Check if the robot's health is at 0 = dead
            if (HealthScript.Dead && !firstDeath)
            {
                IsDead();
            }
        }
        else if(other.tag == "Hammer")
        {
            //Boss takes less damage than other robots
            if (OverworldGameController.gameInfo.getBossStatus())
            {
                HealthScript.TakeDamage(5);
            }
            else
            {
                HealthScript.TakeDamage(8);
            }

            //Check if the robot's health is at 0 = dead
            if (HealthScript.Dead && !firstDeath)
            {
                IsDead();
            }
        }
        else if(other.tag == "Oil Spill")
        {
            //Boss takes less damage than other robots
            if (OverworldGameController.gameInfo.getBossStatus())
            {
                HealthScript.TakeDamage(8);
            }
            else
            {
                HealthScript.TakeDamage(10);
            }

            //Check if the robot's health is at 0 = dead
            if (HealthScript.Dead && !firstDeath)
            {
                IsDead();
            }
        }
        else if(other.tag == "Bomb")
        {
            Debug.Log("Explosion should play");
            if (!explosionParticles.isPlaying)
                explosionParticles.Play();
            Destroy(other.gameObject);

            //Boss takes less damage than other robots
            if (OverworldGameController.gameInfo.getBossStatus())
            {
                HealthScript.TakeDamage(10);
            }
            else
            {
                HealthScript.TakeDamage(15);
            }

            //Check if the robot's health is at 0 = dead
            if (HealthScript.Dead && !firstDeath)
            {
                IsDead();
            }
        }
        else if(other.tag == "BigBomb")
        {
            Debug.Log("Explosion should play");
            if (!explosionParticles.isPlaying)
                explosionParticles.Play();
            Destroy(other.gameObject);

            //Boss takes less damage than other robots
            if (OverworldGameController.gameInfo.getBossStatus())
            {
                HealthScript.TakeDamage(10);
            }
            else
            {
                HealthScript.TakeDamage(20);
            }

            //Check if the robot's health is at 0 = dead
            if (HealthScript.Dead && !firstDeath)
            {
                IsDead();
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

        //Check if boss
        if (OverworldGameController.gameInfo.getBossStatus() && !OverworldGameController.gameInfo.bossFixed)
        {
            //Drop a tool here
            Instantiate(BBPickUp, Vector3.zero, Quaternion.identity);
            OverworldGameController.gameInfo.bossFixed = true;
        }
        else
        {
            if (!OverworldGameController.gameInfo.isSceneLoading())
            {
                GameObject.FindGameObjectWithTag("GamePieces").transform.GetChild(0).gameObject.GetComponent<FixItPlayerController>().setRobotPanel(true);
                //code in progress
                OverworldGameController.gameInfo.changeScene();
            }
        }
    }
}