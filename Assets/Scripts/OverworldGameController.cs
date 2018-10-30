using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverworldGameController : MonoBehaviour {

    public static OverworldGameController gameInfo;
    private static bool created = false;

    private string enemyRobot;
    private int[] treasure;

    public GameObject chestPromptPanel;
    private Text enterKeyPrompt;

    private void Start()
    {
        gameInfo = this;
        enemyRobot = "Common Robot";
        treasure = new int [] {1,1,1,1,1};
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
        if (treasure[chestNum] == 1)
        {
            treasure[chestNum] = 0;
            return true;
        }

        else
            return false;
    }

    //check if player has the weapon they are trying to equip
    //may not need this
    public bool hasWeapon(int chestNum_in)
    {
        if (treasure[chestNum_in] == 0)
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
}
