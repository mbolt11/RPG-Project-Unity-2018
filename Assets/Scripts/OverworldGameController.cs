using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverworldGameController : MonoBehaviour {

    //For singleton
    public static OverworldGameController gameInfo;
    private static bool created = false;

    //for keeping track of villager positions
    public GameObject[] villagers;

    //For keeping track of the tools
    private string[] treasureName = {"Hammer","Oil","Bomb"};
    public string[] toolsfound;
    public int numToolsFound;

    //The tools that are currently checked and need to be taken to the Fit It world
    [HideInInspector]
    public List<GameObject> selectedTools;
   
    //Canvas elements (intitialized in InitializeGameObjects which is called in Awake of MenuController
    private GameObject chestPromptPanel;
    private GameObject menuPanel;
    private Text enterKeyPrompt;

    //To access the actual tool prefabs
    public GameObject Hammer;
    public GameObject Oil;
    public GameObject Wrench;
    public GameObject BigBomb;
    public GameObject Bomb;

    //Toggle counts for each weapon 
    private int wrenchToggleCount = 0;
    private int hammerToggleCount = 1;
    private int oilToggleCount = 1;
    private int bombToggleCount = 1;
    private int bigBombToggleCount = 1;

    //Current enemy type and tool/weapon type
    private string enemyRobot;
    private string currentWeapon;

    //Boolean flags for boss robots
    private bool isBoss;
    public bool bossFixed = false;

    private void Start()
    {
        //At the beginning of the game, the tools menu has only wrench and it is selected
        toolsfound = new string[10];
        toolsfound[0] = "Wrench";
        numToolsFound = 1;

        //store villagers in array
        villagers = GameObject.FindGameObjectsWithTag("Villager");

        //Defaults for entering Fix-it world
        enemyRobot = "Common Robot";
        currentWeapon = "Wrench";
        isBoss = false;
    }

    void Awake()
    {
        //Creates the singleton
        if (!created)
        {
            gameInfo = this;
            DontDestroyOnLoad(gameObject);
            created = true;
        }
        //Destroys the extra copy when returning to Overworld
        else if(created)
        {
            Destroy(gameObject);
        }

        //Initialize canvas gameobjects when the game starts
        InitializeGameObjects();

        enterKeyPrompt = chestPromptPanel.GetComponentInChildren<Text>();
        enterKeyPrompt.text = "Enter Key Prompt";
        chestPromptPanel.SetActive(false);
    }

    //Initialize menu panel and chest prompt panel
    public void InitializeGameObjects()
    {
        menuPanel = GameObject.FindWithTag("Canvas").transform.GetChild(6).gameObject;
        chestPromptPanel = GameObject.FindWithTag("Canvas").transform.GetChild(2).gameObject;
    }

    public OverworldGameController getSingleton()
    {
        return gameInfo;
    }

    //For yellow "boss robots"
    public void setBossStatus(bool isBoss)
    {
        this.isBoss = isBoss;
    }

    public bool getBossStatus()
    {
        return isBoss;
    }

    public void setEnemyRobot(string enemyName)
    {
        if (enemyName == "Common Robot")
            enemyRobot = enemyName;
        else if (enemyName == "Outside Robot")
            enemyRobot = enemyName;
    }

    public void openChest(int chestNum)
    {
        //Add this tool to the toolsfound array
        toolsfound[numToolsFound] = treasureName[chestNum - 1];

        //Make this treasure available in the menu
        menuPanel.GetComponent<MenuController>().ActivateToolInMenu(treasureName[chestNum-1],numToolsFound);

        //Increment the total number of tools found so far
        numToolsFound++;
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

    public void ChooseFunction(Toggle theToggle)
    {
        string weapon = theToggle.GetComponentInChildren<Text>().text;
        int togglecount = 0;

        switch(weapon)
        {
            case "Wrench":
                togglecount = wrenchToggleCount;
                Debug.Log("Wrench: " + togglecount);
                wrenchToggleCount++;
                break;
            case "Hammer":
                togglecount = hammerToggleCount;
                Debug.Log("Hammer: " + togglecount);
                hammerToggleCount++;
                break;
            case "Oil":
                togglecount = oilToggleCount;
                Debug.Log("Oil: " + togglecount);
                oilToggleCount++;
                break;
            case "Bomb":
                togglecount = bombToggleCount;
                Debug.Log("Bomb: " + togglecount);
                bombToggleCount++;
                break;
            case "Big Bomb":
                togglecount = bigBombToggleCount;
                Debug.Log("Big Bomb: " + togglecount);
                bigBombToggleCount++;
                break;
        }
        if(togglecount%2==1)
        {
            //Try to add weapon, will return false if player has 4 tools already
            //prompting toggle to be turned back off
            if(!AddTool(weapon))
            {
                theToggle.isOn = false;
            }
        }
        else
        {
            RemoveTool(weapon);
        }
    }
    //add chosen item to selectedTools array
    public bool AddTool(string toolName)
    {
        if(selectedTools.Count < 4)
        {
            switch (toolName)
            {
                case "Wrench":
                    selectedTools.Add(Wrench);
                    break;
                case "Hammer":
                    selectedTools.Add(Hammer);
                    break;
                case "Oil":
                    selectedTools.Add(Oil);
                    break;
                case "Big Bomb":
                    selectedTools.Add(BigBomb);
                    break;
                case "Bomb":
                    selectedTools.Add(Bomb);
                    break;
            }
            Debug.Log("List after add:\n");
            printSelectedTools();
            return true;
        }
        Debug.Log("Already Had Four Items");
        return false;
    }

    public void RemoveTool(string toolName)
    {
        for(int i = 0; i < selectedTools.Count; i++)
        {
            if (selectedTools[i].name == toolName)
                selectedTools.RemoveAt(i);
        }
        Debug.Log("List after removal:\n");
        printSelectedTools();
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

    //To be called when entering play mode and returning to the Overworld
    public void initializeSelectedTools()
    {
        wrenchToggleCount = 0;
        hammerToggleCount = 1;
        oilToggleCount = 1;
        bombToggleCount = 1;
        bigBombToggleCount = 1;

        selectedTools = new List<GameObject>();
        selectedTools.Add(Wrench);
    }

    //returns a string of the weapon currently equipped in Fix-It
    public string getCurrentWeapon()
    {
        return currentWeapon;
    }

    public void setCurrentWeapon(string weaponIn)
    {
        currentWeapon = weaponIn;
    }
}
