using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fido : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
    {
        if (OverworldGameController.gameInfo.gameOver)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
