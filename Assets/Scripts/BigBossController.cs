using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BigBossController : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
    {
        if(SceneManager.GetActiveScene().name == "Overworld")
        {
            gameObject.SetActive(false);
            GetComponent<RobotThrowsParts>().enabled = false;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.Rotate(0,180,0);
            //transform.position = new Vector3(transform.position.x, -.75f, transform.position.z);
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
}
