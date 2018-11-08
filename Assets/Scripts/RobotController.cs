using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RobotController : MonoBehaviour
{
    public GameObject robotBody;
    public GameObject BigBomb;
    public ParticleSystem explosionParticles;

    [HideInInspector]
    public bool isBoss;

    private Health HealthScript;

    // Use this for initialization
    void Start()
    {
        //Various setup things
        Scene currentScene = SceneManager.GetActiveScene();

        //This sets random bosses?
        int rand = Random.Range(0, 4);
        isBoss = rand == 0 ? true : false;

        HealthScript = GetComponentInParent<Health>();

        //If we are in the overworld
        if(currentScene.name == "Overworld")
        {
            GetComponent<RobotThrowsParts>().enabled = false;
        }
        //If we are in the Fix It World
        else
        {
            GetComponent<RobotThrowsParts>().enabled = true;
        }

        //Color robot if it's a boss
        //Health or movement should be different in the future
        if (isBoss)
        {
            robotBody.transform.GetComponent<Renderer>().material.color = new Color(.83f, .69f, .22f, 1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //When the robot gets hit by a tool/weapon
        if (other.tag == "Wrench" || other.tag == "Oil Spill" || other.tag == "Bomb" || other.tag == "BigBomb")
        {
            //Play explosion and destroy tool
            explosionParticles.Play();
            Destroy(other.gameObject);

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
            if (HealthScript.Dead)
            {
                //Change robot color to green to indicate fixed
                robotBody.transform.GetComponent<Renderer>().material.color = Color.green;

                //Stop throwing parts
                GetComponent<RobotThrowsParts>().enabled = false;

                //Check if boss
                if (OverworldGameController.gameInfo.getBossStatus())
                {
                    //Drop a tool here
                    Instantiate(BigBomb, Vector3.zero, Quaternion.identity);
                }
                else
                {
                    //Should show a message and go back to overworld here
                }
            }
        }
    }
}