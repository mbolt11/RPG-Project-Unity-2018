using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FixItPlayerController : MonoBehaviour
{
    public GameObject playerBody;
    public GameObject bossPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CommonPart")
        {
            //Debug.Log("part hit");
            playerBody.transform.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (other.tag == "OutsidePart")
        {
            //Debug.Log("part hit");
            playerBody.transform.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if(other.tag=="BigBomb")
        {
            //show a message?
            bossPanel.SetActive(true);
            OverworldGameController.gameInfo.bossFixed = true;

            Destroy(other.gameObject);

            //code in progress
            /*SceneManager.LoadScene("Overworld");

            //set canvas childs to active
            GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
            
            for(int i = 1; i < 7; i++)
            {
                canvas.transform.GetChild(i).gameObject.SetActive(true);
                Debug.Log(canvas.transform.GetChild(i).gameObject.name);
            }

            OverworldGameController.gameInfo.AddTool("BigBomb");*/
        }
        //reduce health of the player
        else if (other.tag == "Oil Spill")
        {
            Destroy(other.gameObject);
        }
    }

}
