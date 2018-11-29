using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour 
{
    //UI toggle gameobjects for each tool
    public GameObject[] tools;

    //This calls the function in OverworldGameController to initialize the gameobjects when the scene is reloaded
    private void Awake()
    {
        OverworldGameController.gameInfo.InitializeGameObjects();
    }

    // Use this for initialization
    void Start () 
    {
        gameObject.SetActive(false);

        //If coming back after beating boss robot, add Big Bomb to menu
        if (OverworldGameController.gameInfo.bossFixed && !OverworldGameController.gameInfo.bombAdded)
        {
            //OverworldGameController.gameInfo.bossFixed = false;
            OverworldGameController.gameInfo.bombAdded = true;

            //Will not work for all pickups-- must remember to add each pickup to the toolsfound array
            OverworldGameController.gameInfo.toolsfound[OverworldGameController.gameInfo.numToolsFound] = "Big Bomb";
            OverworldGameController.gameInfo.numToolsFound++;

            //If all the tools have been found, make big boss show up
            if (OverworldGameController.gameInfo.numToolsFound > 4)
            {
                OverworldGameController.gameInfo.EnableBigBoss();
            }
        }

        //When the Overworld loads, initialize the menu panel with the amount of tools that have be acquired so far
        for (int i = 0; i < OverworldGameController.gameInfo.numToolsFound; i++)
        {
            ActivateToolInMenu(OverworldGameController.gameInfo.toolsfound[i],i);
        }
        for (int i = OverworldGameController.gameInfo.numToolsFound; i < tools.Length; i++)
        {
            tools[i].SetActive(false);
        }

        //Set/Reset the toggle counts and selectedTools list to correspond with the initialized menu
        OverworldGameController.gameInfo.initializeSelectedTools();

        //If you are coming back after beating the boss
        if(OverworldGameController.gameInfo.gameOver)
        {
            OverworldGameController.gameInfo.EndOfGame();
        }

    }

    //Add to menu on initialization/when new tool is acquired
    public void ActivateToolInMenu(string toolname, int index)
    {
        tools[index].GetComponentInChildren<Text>().text = toolname;
        tools[index].SetActive(true);
    }
}
