using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldGameController : MonoBehaviour {

    public static OverworldGameController gameInfo;
    private static bool created = false;

    private string enemyRobot;

    private void Start()
    {
        gameInfo = this;
        enemyRobot = "Common Robot";
    }

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            //Debug.Log("Awake: " + this.gameObject);
        }
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

    public string getEnemyID()
    {
        return enemyRobot;
    }
}
