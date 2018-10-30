﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public GameObject[] tools;
    private int toolcounter = 1;

    // Use this for initialization
	void Start () 
    {
        gameObject.SetActive(false);

        //At the start of the game, only the first tool is activated
        tools[0].SetActive(true);
        for (int i = 1; i < tools.Length; i++)
        {
            tools[i].SetActive(false);
        }
    }

    //Add to menu when new tool is acquired
    public void ActivateToolInMenu(string toolname)
    {
        tools[toolcounter].GetComponentInChildren<Text>().text = toolname;
        tools[toolcounter].SetActive(true);
        toolcounter++;
    }
}
