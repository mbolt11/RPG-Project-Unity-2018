using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixItPlayerController : MonoBehaviour
{
    public GameObject playerBody;

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
    }

}
