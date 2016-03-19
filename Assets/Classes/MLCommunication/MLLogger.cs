using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum LevelStat {

    //TODO:Specify LevelStats
    LevelTime, NumberOfTrials, PlayerTravelDistance, EstimatedDifficulty, GuardianSpeed,
    GuardianAwarenessRange, GuardianReactionTime
}

/// <summary>
/// Used to update MLLevelStats which keeps some statistical data about current level gameplay of user.
/// </summary>
public class MLLogger {

    protected static Dictionary<LevelStat, float> stats = new Dictionary<LevelStat, float>();
    //protected static MLLevelStats currentStats;

    public static void incrementStat( LevelStat stat, float value) {

        //TODO:Implement
    }

    public static void decrementStat( LevelStat stat, float value) {

        //TODO:Implement
    }

    public static void setStat( LevelStat stat, float value) {

        //TODO:Implement
    }
}
