using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum PlayStat {

    LevelNo, LevelTime, NumberOfTrials, PlayerTravelDistance, Result
}

public enum SurveyStat {

    PlayerId, EstimatedDifficulty
}

/// <summary>
/// Used to update MLLevelStats which keeps some statistical data about current level gameplay of user.
/// </summary>
public class MLLogger {

    protected static Dictionary<PlayStat, float> playStats;
    protected static Dictionary<SurveyStat, float> surveyStats;

    public static void Init() {

        playStats = new Dictionary<PlayStat, float>();
        foreach (PlayStat val in Enum.GetValues(typeof(PlayStat))) {
            playStats.Add(val, Constants.INVALID_STAT);
        }
        surveyStats = new Dictionary<SurveyStat, float>();
        foreach (SurveyStat val in Enum.GetValues(typeof(SurveyStat))) {
            surveyStats.Add(val, Constants.INVALID_STAT);
        }
    }

    /// <summary>
    /// Note:Make sure stat value is set before increment or decrement
    /// </summary>
    /// <param name="stat">LevelStat value</param>
    /// <param name="value">float value</param>
    public static void IncrementStat( PlayStat stat, float value) {

        playStats[stat] += value;
    }

    /// <summary>
    /// Note:Make sure stat value is set before increment or decrement
    /// </summary>
    /// <param name="stat">LevelStat value</param>
    /// <param name="value">float value</param>
    public static void DecrementStat( PlayStat stat, float value) {

        playStats[stat] -= value;
    }

    public static void SetStat( PlayStat stat, float value) {

        playStats[stat] = value;
    }

    /// <summary>
    /// Note:Make sure stat value is set before increment or decrement
    /// </summary>
    /// <param name="stat">SurveyStat value</param>
    /// <param name="value">float value</param>
    public static void IncrementStat(SurveyStat stat, float value) {

        surveyStats[stat] += value;
    }

    /// <summary>
    /// Note:Make sure stat value is set before increment or decrement
    /// </summary>
    /// <param name="stat">SurveyStat value</param>
    /// <param name="value">float value</param>
    public static void DecrementStat(SurveyStat stat, float value) {

        surveyStats[stat] -= value;
    }

    public static void SetStat(SurveyStat stat, float value) {

        surveyStats[stat] = value;
    }

    public static Dictionary<PlayStat, float> GetPlayStats() {

        return playStats;
    }

    public static Dictionary<SurveyStat, float> GetSurveyStats() {

        return surveyStats;
    }
}
