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
            //OverworldGameController.gameInfo.AddTool("BigBomb");
            //OverworldGameController.gameInfo.bossFixed = true;

            Destroy(other.gameObject);
            SceneManager.LoadScene("Overworld");
        }
        //reduce health of the player
        else if (other.tag == "Oil Spill")
        {
            Destroy(other.gameObject);
        }
    }

}
