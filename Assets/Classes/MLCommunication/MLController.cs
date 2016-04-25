using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class MLController {

    public static float Difficulty = 50.0f;
    

    public static void AdjustDifficulty() {

        float previousDifficulty = Difficulty;

        float currentDifficulty;

        currentDifficulty = MLCommunicator.predictDifficulty();

        SetLevelStats(currentDifficulty);
    }

    public static void SetLevelStats( float currentDifficulty) {

        List<LevelStat> stat;
        List<LevetStatMin> statMin;
        List<LevelStatMax> statMax;
        stat = new List<LevelStat>(Enum.GetValues(typeof(LevelStat)).Cast<LevelStat>());
        statMin = new List<LevetStatMin>(Enum.GetValues(typeof(LevetStatMin)).Cast<LevetStatMin>());
        statMax = new List<LevelStatMax>(Enum.GetValues(typeof(LevelStatMax)).Cast<LevelStatMax>());
        for( int i = 0; i < stat.Count; i++) {
            float previous = MLLevelStats.GetStat(stat[i]);
            float min = MLLevelStats.GetMinStat(statMin[i]);
            float max = MLLevelStats.GetMaxStat(statMax[i]);

            float value = ((max - min) / 100) * currentDifficulty;
            value += min;
            MLLevelStats.SetStat(stat[i], value);
        }
    }
}