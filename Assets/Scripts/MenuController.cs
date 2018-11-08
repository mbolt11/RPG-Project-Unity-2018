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

        //If coming back after beating boss robot, add Big Bomb to menu
        if (OverworldGameController.gameInfo.bossFixed)
        {
            ActivateToolInMenu("Big Bomb",OverworldGameController.gameInfo.numToolsFound);
            OverworldGameController.gameInfo.bossFixed = false;

            //Will not work for all pickups-- must remember to add each pickup to the toolsfound array
            OverworldGameController.gameInfo.toolsfound[OverworldGameController.gameInfo.numToolsFound] = "Big Bomb";
            OverworldGameController.gameInfo.numToolsFound++;
        }
    }

    //Add to menu on initialization/when new tool is acquired
    public void ActivateToolInMenu(string toolname, int index)
    {
        tools[index].GetComponentInChildren<Text>().text = toolname;
        tools[index].SetActive(true);
    }
}
