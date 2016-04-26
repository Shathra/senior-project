using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

class GameController {

    protected static Level currentLevel;
    public static bool gameOver;
    public static bool gameWon;

    private static readonly int maxNumberOfTrial = 1;
    private static int numberOfTrial;

    //From functions
    private static float levelTime;
    public static void Init() {
        gameWon = false;
        gameOver = false;
        currentLevel = GameObject.FindGameObjectWithTag("Level").GetComponent<Level>();

        numberOfTrial = 0;
    }

    public static void GameOver() {

        levelTime = (float)currentLevel.GetElapsedTime();
        MLLogger.SetStat(PlayStat.LevelTime, levelTime);
        MLLogger.SetStat(PlayStat.Result, gameWon ? 1 : 0);
        MLLogger.SetStat(PlayStat.NumberOfTrials, numberOfTrial);
        //MLLogger.SetStat(PlayStat.LevelNo, currentLevel.GetId());
        //MLCommunicator.writeMLTrainFile();

        if ( numberOfTrial == maxNumberOfTrial) {

            numberOfTrial = 10;
            MLLogger.SetStat(PlayStat.Result, 1);
            MLLogger.SetStat(PlayStat.NumberOfTrials, numberOfTrial);
            MLLogger.SetStat(PlayStat.PlayerTravelDistance, 100);
            MLLogger.SetStat(PlayStat.LevelTime, 300);
            MLController.AdjustDifficulty();
            numberOfTrial = 0;
            MLLogger.ClearPlayStats();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public static void GameWon()
    {
        gameWon = true;
        gameOver = true;
        Debug.Log("GAME WON :D");
        //Debug.Break();
        GameOver();
    }
    public static void GameLost()
    {
        gameWon = false;
        gameOver = true;

        numberOfTrial++;
        
        Debug.Log("GAME LOST :(");
        //Debug.Break();
        GameOver();
    }


}