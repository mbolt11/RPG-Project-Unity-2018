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
    void Start()
    {
        firstDeath = false;

        //Various setup things
        Scene currentScene = SceneManager.GetActiveScene();

        HealthScript = GetComponentInParent<Health>();

        //If we are in the overworld
        if(currentScene.name == "Overworld")
        {
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
        }

        //Color robot if it's a boss
        //Health or movement should be different in the future
        if (isBoss)
        {
            robotBody.transform.GetComponent<Renderer>().material.color = new Color(.83f, .69f, .22f, 1f);
        }
        else if (tag == "Common Robot")
        {
            robotBody.transform.GetComponent<Renderer>().material.color = new Color(1f, .033f, .0141f, 1f);
        }
        else if (tag == "Outside Robot")
        {
            robotBody.transform.GetComponent<Renderer>().material.color = new Color(0.318f, 0.585f, .0118f, 1f);
        }
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

            //Reduce health of robot.. can update this to be different for different kinds of weapons in the future
            //Boss is harder to beat than other robots
            if (OverworldGameController.gameInfo.getBossStatus())
            {
                HealthScript.TakeDamage(5);
            }
            else
            {
                HealthScript.TakeDamage(10);
            }

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

                //Check if boss
                if (OverworldGameController.gameInfo.getBossStatus() && !OverworldGameController.gameInfo.bossFixed)
                {
                    Debug.Log("Defeated a boss robot");
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
    }
}