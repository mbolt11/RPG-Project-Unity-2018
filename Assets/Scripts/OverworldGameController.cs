using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OverworldGameController : MonoBehaviour 
{
    //For singleton
    public static OverworldGameController gameInfo;
    private static bool created = false;

    //For keeping track of the tools
    private string[] treasureName = {"Hammer","Oil","Bomb"};
    private List<string> treasuresOpened = new List<string>();
    public string[] toolsfound;
    public int numToolsFound;

    //The tools that are currently checked and need to be taken to the Fit It world
    [HideInInspector]
    public List<GameObject> selectedTools;
   
    //Canvas elements (intitialized in InitializeGameObjects which is called in Awake of MenuController
    private GameObject chestPromptPanel;
    private GameObject menuPanel;
    private Text enterKeyPrompt;
    private DialogueManager dialogueManager;

    //To access the actual tool prefabs
    public GameObject Hammer;
    public GameObject Oil;
    public GameObject Wrench;
    public GameObject BigBomb;
    public GameObject Bomb;

    //Villager and big boss
    public GameObject Villager;
    public GameObject BigBoss;
    public GameObject Fido;
    public bool bossAlive = false;

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
    public bool bombAdded = false;

    //For scene loading
    private bool sceneLoading = false;
    private int sceneNumber;

    //Reference to the main camera
    private GameObject userCamera;

    //To save player Overworld location
    public Vector3 playerlocation;

    [HideInInspector]
    public string robotHitName;
    public List<string> robotsBeaten = new List<string>();

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
        else if (created)
        {
            Destroy(gameObject);
        }

        //Initialize canvas gameobjects when the game starts
        InitializeGameObjects();
    }

    private void Start()
    {
        //At the beginning of the game, the tools menu has only wrench and it is selected
        toolsfound = new string[10];
        toolsfound[0] = "Wrench";
        numToolsFound = 1;

        //Defaults for entering Fix-it world
        enemyRobot = "Common Robot";
        if (selectedTools.Count > 0)
            currentWeapon = selectedTools[0].name;
        else
            currentWeapon = "Wrench";
        isBoss = false;

        //At the start of the game the player is at 3, 0, -3
        playerlocation = new Vector3(3, 0, -3);

        //Disable Fido at the beginning of the game
        Fido.SetActive(false);

        //For the starting narration
        dialogueManager.BeginMessage();

    }

    //This method gets called by awake function of MenuController so that it happens every time you enter the overworld
    public void InitializeGameObjects()
    {
        //Initialize canvas panels/objects
        menuPanel = GameObject.FindWithTag("Canvas").transform.GetChild(6).gameObject;
        chestPromptPanel = GameObject.FindWithTag("Canvas").transform.GetChild(2).gameObject;
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        enterKeyPrompt = chestPromptPanel.GetComponentInChildren<Text>();
        enterKeyPrompt.text = "Enter Key Prompt";
        chestPromptPanel.SetActive(false);

        //Initialize the Big Boss and Fido
        BigBoss = GameObject.FindGameObjectWithTag("BigBoss");
        Fido = GameObject.FindGameObjectWithTag("Fido");

        //For returning to overworld if big boss has been enabled already
        if(bossAlive)
        {
            BigBoss.SetActive(true);
            Fido.SetActive(true);
        }

        //Make sure that only the chests that have been opened are deactivated
        for (int i = 0; i < treasuresOpened.Count; i++)
        {
            GameObject.Find(treasuresOpened[i]).SetActive(false);
        }

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
        enemyRobot = enemyName;
    }

    public string OpenChest(int chestNum)
    {
        //Add this tool to the toolsfound array and add chest to treasuresOpened array
        toolsfound[numToolsFound] = treasureName[chestNum - 1];
        string chestname = "Chest" + chestNum;
        treasuresOpened.Add(chestname);

        //Make this treasure available in the menu
        menuPanel.GetComponent<MenuController>().ActivateToolInMenu(treasureName[chestNum-1],numToolsFound);

        //Increment the total number of tools found so far
        numToolsFound++;

        //If all the tools have been found, make big boss show up
        if(numToolsFound > 4)
        {
            EnableBigBoss();
        }

        //Return the name of the treasure found to use in the message
        return treasureName[chestNum - 1];
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
                wrenchToggleCount++;
                break;
            case "Hammer":
                togglecount = hammerToggleCount;
                hammerToggleCount++;
                break;
            case "Oil":
                togglecount = oilToggleCount;
                oilToggleCount++;
                break;
            case "Bomb":
                togglecount = bombToggleCount;
                bombToggleCount++;
                break;
            case "Big Bomb":
                togglecount = bigBombToggleCount;
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

            //Big bomb has a different case because the string has a space
            if(toolName == "Big Bomb" && selectedTools[i].name == "BigBomb")
            {
                selectedTools.RemoveAt(i);
            }
        }

        //Update the current weapon selected when you first enter fix-it
        if(selectedTools.Count > 0)
            currentWeapon = selectedTools[0].name;

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
        currentWeapon = "Wrench";
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

    public void sceneIsLoading()
    {
        sceneLoading = true;
    }

    public void finishedLoading()
    {
        sceneLoading = false;
    }

    public bool isSceneLoading()
    {
        return sceneLoading;
    }

    public int changeSceneNum()
    {
        Debug.Log(sceneNumber);
        sceneNumber++;

        if (sceneNumber > 1)
            sceneNumber = 0;

        return sceneNumber;
    }

    public void changeScene()
    {
        sceneIsLoading();
        findPlayerCamera();

        //Debug.Log("Before switch" + sceneNumber);
        sceneNumber++;

        if (sceneNumber > 1)
        {
            sceneNumber = 0;
        }
        //Debug.Log("after switch" + sceneNumber);

        //Debug.Log("camera " + userCamera);
        userCamera.GetComponent<TransitionScript>().StartTransition();
        StartCoroutine(WaitFor());
    }

    IEnumerator WaitFor()
    {
        yield return new WaitForSeconds(3);

        //Load the fix it scene
        SceneManager.LoadScene(sceneNumber);
        finishedLoading();
    }

    public void findPlayerCamera()
    {
        if (sceneNumber == 0)
        {
            //userCamera = GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).gameObject;
            userCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        else
        {
            //userCamera = GameObject.FindGameObjectWithTag("GamePieces").transform.GetChild(0).GetChild(2).gameObject;
            userCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    public void RobotDefeated()
    {
        //Takes the name of the robot most recently hit in the overworld and adds it to the list
        robotsBeaten.Add(robotHitName);
    }

    //Method to enable the big boss which is called once all the tools have been acquired
    public void EnableBigBoss()
    {
        BigBoss.SetActive(true);
        dialogueManager.BigBossEnabledMessage();
        bossAlive = true;
    }

    //Method to wrap up game when Big Boss is defeated
    public void EndOfGame()
    {
        //Fido.SetActive(true);
        dialogueManager.YouWonMessage();
    }
}
