using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverworldGameController : MonoBehaviour {

    public static OverworldGameController gameInfo;
    private static bool created = false;

    private string enemyRobot;
    private bool isBoss;
    private int[] treasureNumber;

    //For keeping track of the tools
    private string[] treasureName = {"Hammer","Oil","Bomb"};//"Big Bomb" needs to come from a boss

    [HideInInspector]
    public List<GameObject> selectedTools;
   
    public GameObject chestPromptPanel;
    public GameObject menuPanel;
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

    private string currentWeapon;

    //boolean flag for if boss has been fixed
    public bool bossFixed = false;


    private void Start()
    {
        gameInfo = this;
        enemyRobot = "Common Robot";
        isBoss = false;
        selectedTools = new List<GameObject>();
        selectedTools.Add(Wrench);

        //For testing
        selectedTools.Add(Oil);

        currentWeapon = "Wrench";
        treasureNumber = new int [] {1,1,1,1,1};
    }

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(gameObject);
            created = true;
        }

        enterKeyPrompt = chestPromptPanel.GetComponentInChildren<Text>();
        enterKeyPrompt.text = "Enter Key Prompt";
        chestPromptPanel.SetActive(false);
    }

    void Update()
    {
        if (bossFixed)
        {
            menuPanel.GetComponent<MenuController>().ActivateToolInMenu("BigBomb");
            bossFixed = false;
        }
    }

    public OverworldGameController getSingleton()
    {
        return gameInfo;
    }

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

    public void ChooseFunction(GameObject label)
    {
        string weapon = label.GetComponent<Text>().text;
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
            case "BigBomb":
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
                label.GetComponentInParent<Toggle>().isOn = false;
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
                case "BigBomb":
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
