using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class GameController {

    protected static Level currentLevel;
    public static bool gameWon;
    public static bool gameLost;

    public static void Init() {
        gameWon = false;
        gameLost = false;
        currentLevel = GameObject.FindGameObjectWithTag("Level").GetComponent<Level>();
    }

    public static void GameOver() {

        float levelTime = (float)currentLevel.GetElapsedTime();
        MLLogger.SetStat(LevelStat.LevelTime, levelTime);
    }
    
    public static void GameWon()
    {
        gameWon = true;
        Debug.Log("GAME WON :D");
        Debug.Break();
        GameOver();
    }
    public static void GameLost()
    {
        gameLost = true;
        Debug.Log("GAME LOST :(");
        Debug.Break();
        GameOver();
    }


}