using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverworldGameController : MonoBehaviour {

    public static OverworldGameController gameInfo;
    private static bool created = false;

    private string enemyRobot;
    private int[] treasureNumber;
    //"Big Bomb" needs come from a boss
    private string[] treasureName = {"Hammer","Oil","Bomb"};
    //private GameObject[] selectedTools;
    private List<GameObject> selectedTools;
    public GameObject chestPromptPanel;
    public GameObject menuPanel;
    private Text enterKeyPrompt;

    public GameObject hammer;
    public GameObject oil;
    public GameObject wrench;
    public GameObject bigBomb;
    public GameObject bomb;


    private void Start()
    {
        gameInfo = this;
        enemyRobot = "Common Robot";
        selectedTools = new List<GameObject>();
        selectedTools.Add(wrench);
        treasureNumber = new int [] {1,1,1,1,1};
    }

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }

        enterKeyPrompt = chestPromptPanel.GetComponentInChildren<Text>();
        enterKeyPrompt.text = "Enter Key Prompt";
        chestPromptPanel.SetActive(false);
    }

    public OverworldGameController getSingleton()
    {
        return gameInfo;
    }

    public void setEnemyRobot(string enemyName)
    {
        if (enemyName == "Common Robot")
            enemyRobot = enemyName;
        else if (enemyName == "Outside Robot")
            enemyRobot = enemyName;
    }

    public bool openChest(int chestNum)
    {
        if (treasureNumber[chestNum] == 1)
        {
            treasureNumber[chestNum] = 0;

            //Make this treasure available in the menu
            menuPanel.GetComponent<MenuController>().ActivateToolInMenu(treasureName[chestNum-1]);

            return true;
        }

        else
            return false;
    }

    //check if player has the weapon they are trying to equip
    //may not need this
    public bool hasWeapon(int chestNum_in)
    {
        if (treasureNumber[chestNum_in] == 0)
            return true;
        return false;
    }

    public void EnterKeyTextAppear()
    {
        enterKeyPrompt.text = "Press ENTER to open";
        chestPromptPanel.SetActive(true);
    }

    public void EnterKeyTextDisappear()
    {
        chestPromptPanel.SetActive(false);
    }

    public string getEnemyID()
    {
        return enemyRobot;
    }

    //add chosen item to selectedTools array
    public void addTool(string toolName)
    {
        if (selectedTools.Count < 4)
        {
            switch (toolName)
            {
                case "wrench":
                    selectedTools.Add(wrench);
                    break;
                case "hammer":
                    selectedTools.Add(hammer);
                    break;
                case "oil":
                    selectedTools.Add(oil);
                    break;
                case "bigBomb":
                    selectedTools.Add(bigBomb);
                    break;
                case "bomb":
                    selectedTools.Add(bomb);
                    break;
            }
        }
    }

    public void removeTool(string toolName)
    {
        for(int i = 0; i < selectedTools.Count; i++)
        {
            if (selectedTools[i].name == toolName)
                selectedTools.RemoveAt(i);
        }
    }

    public void printSelectedTools()
    {
        string express = "";
        for (int i = 0; i < selectedTools.Count; i++)
        {
            express += selectedTools[i].name + " ";
        }

        Debug.Log(express);
    }
}
