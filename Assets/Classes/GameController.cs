using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class GameController {

    protected static Level currentLevel;

    public static void Init() {

        currentLevel = GameObject.FindGameObjectWithTag("Level").GetComponent<Level>();
    }

    public static void GameOver() {

        float levelTime = (float)currentLevel.GetElapsedTime();
        MLLogger.SetStat(LevelStat.LevelTime, levelTime);
    }


}