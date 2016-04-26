using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class MLController {

    public static float Difficulty = 50.0f;
    

    public static void AdjustDifficulty() {

        float previousDifficulty = Difficulty;

        float currentDifficulty;

        currentDifficulty = MLCommunicator.predictDifficulty();

        SetLevelStats( previousDifficulty, currentDifficulty);

    }

    public static void SetLevelStats( float previousDifficulty, float currentDifficulty) {

        List<LevelStat> stat;
        List<LevetStatMin> statMin;
        List<LevelStatMax> statMax;
        stat = new List<LevelStat>(Enum.GetValues(typeof(LevelStat)).Cast<LevelStat>());
        statMin = new List<LevetStatMin>(Enum.GetValues(typeof(LevetStatMin)).Cast<LevetStatMin>());
        statMax = new List<LevelStatMax>(Enum.GetValues(typeof(LevelStatMax)).Cast<LevelStatMax>());

        float scale = (previousDifficulty - 50) / 50;
        currentDifficulty = currentDifficulty + (currentDifficulty * scale);
        Debug.LogWarning(Difficulty + "," + currentDifficulty);
        Difficulty = currentDifficulty;
        currentDifficulty = 100 - currentDifficulty;

        float previous, min, max, value;
        for ( int i = 0; i < stat.Count; i++) {
            previous = MLLevelStats.GetStat(stat[i]);
            min = MLLevelStats.GetMinStat(statMin[i]);
            max = MLLevelStats.GetMaxStat(statMax[i]);

            
            value = ((max - min) / 100) * currentDifficulty;

            value += min;
            MLLevelStats.SetStat(stat[i], value);
        }

        previous = MLLevelStats.GetStat(LevelStat.ShurikenLimit);
        min = MLLevelStats.GetMinStat(LevetStatMin.ShurikenLimit);
        max = MLLevelStats.GetMaxStat(LevelStatMax.ShurikenLimit);
        value = ((max - min) / 100) * (100 - currentDifficulty);
        value += min;

        MLLevelStats.SetStat( LevelStat.ShurikenLimit, value);
    }
}