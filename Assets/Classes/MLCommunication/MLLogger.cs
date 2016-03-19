using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum LevelStat {

    LevelNo, PlayerId, LevelTime, NumberOfTrials, PlayerTravelDistance, EstimatedDifficulty
}

/// <summary>
/// Used to update MLLevelStats which keeps some statistical data about current level gameplay of user.
/// </summary>
public class MLLogger {

    protected static Dictionary<LevelStat, float> stats;

    public static void Init() {

        stats = new Dictionary<LevelStat, float>();
        foreach (LevelStat val in Enum.GetValues(typeof(LevelStat))) {
            stats.Add(val, Constants.INVALID_STAT);
        }
    }

    /// <summary>
    /// Note:Make sure stat value is set before increment or decrement
    /// </summary>
    /// <param name="stat">LevelStat value</param>
    /// <param name="value">float value</param>
    public static void IncrementStat( LevelStat stat, float value) {

        stats[stat] += value;
    }

    /// <summary>
    /// Note:Make sure stat value is set before increment or decrement
    /// </summary>
    /// <param name="stat">LevelStat value</param>
    /// <param name="value">float value</param>
    public static void DecrementStat( LevelStat stat, float value) {

        stats[stat] -= value;
    }

    public static void SetStat( LevelStat stat, float value) {

        stats[stat] = value;
    }
}
