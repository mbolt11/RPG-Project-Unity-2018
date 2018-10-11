using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldGameController : MonoBehaviour {

    public static OverworldGameController gameInfo;

    private string enemyRobot;

    private void Start()
    {
        gameInfo = this;
    }

    public OverworldGameController getSingleton()
    {
        return gameInfo;
    }

    public void setEnemyRobot(string enemyName)
    {
        if (enemyName == "CommonRobot")
            enemyRobot = enemyName;
        else if (enemyName == "OutsideRobot")
            enemyRobot = enemyName;
    }

    public string getEnemyID()
    {
        return enemyRobot;
    }
}
