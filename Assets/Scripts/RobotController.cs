using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RobotController : MonoBehaviour
{
    //Get the robot's main body cube
    public GameObject robotBody;
    public GameObject BigBomb;

    public ParticleSystem explosionParticles;

    [HideInInspector]
    public bool isBoss;

    // Use this for initialization
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int rand = Random.Range(0, 4);
        isBoss = rand == 0 ? true : false;

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

        //color robot if it's a boss
        //health or movement should be different in the future
        if (isBoss)
        {
            robotBody.transform.GetComponent<Renderer>().material.color = new Color(.83f, .69f, .22f, 1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the robot gets hit by a tool/weapon, it turns green to indicate it is fixed
        if (other.tag == "Wrench" || other.tag == "Hammer" || other.tag == "Oil Spill" || other.tag == "Bomb" || other.tag == "BigBomb")
        {
            //Change the color
            explosionParticles.Play();
            robotBody.transform.GetComponent<Renderer>().material.color = Color.green;
            Destroy(other.gameObject);

            //stop throwing parts
            GetComponent<RobotThrowsParts>().enabled = false;

            //check if boss
            if (OverworldGameController.gameInfo.getBossStatus())
            {
                //DROP A TOOL HERE
                Instantiate(BigBomb, Vector3.zero, Quaternion.identity);
            }
        }
    }
}