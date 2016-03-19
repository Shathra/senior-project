using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class GameController {

    protected static Level currentLevel;
    public static bool gameOver;
    public static bool gameWon;

    public static void Init() {
        gameWon = false;
        gameOver = false;
        currentLevel = GameObject.FindGameObjectWithTag("Level").GetComponent<Level>();
    }

    public static void GameOver() {

        float levelTime = (float)currentLevel.GetElapsedTime();
        MLLogger.SetStat(PlayStat.LevelTime, levelTime);
        MLLogger.SetStat(PlayStat.Result, gameWon ? 1 : 0);
        MLLogger.SetStat(PlayStat.LevelNo, currentLevel.GetId());
        MLCommunicator.writeMLTrainFile();
    }
    
    public static void GameWon()
    {
        gameWon = true;
        gameOver = true;
        Debug.Log("GAME WON :D");
        Debug.Break();
        GameOver();
    }
    public static void GameLost()
    {
        gameWon = false;
        gameOver = true;
        Debug.Log("GAME LOST :(");
        Debug.Break();
        GameOver();
    }


}