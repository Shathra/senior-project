
using System.Collections.Generic;

public enum LevelStat {

    //TODO:Specify LevelStats
    LevelTime, LevelNo, GuardianWeaponDamage, GuardianWeaponMissileSpeed, GuardianSpeed,
    GuardianAwarenessRange, GuardianReactionTime
}

/// <summary>
/// Data class for level statistics
/// </summary>
public class MLLevelStats {

    public static float GuardianSpeed = 1.0f;
    public static float GuardianTurnRate = 1.0f;

    private Dictionary<LevelStat, float> stats;

    /// <summary>
    /// Initializes an empty object
    /// </summary>
    public MLLevelStats()
    {
        //TODO:Implement
    }

    /// <summary>
    /// Convert MLLevelStats to JSON
    /// </summary>
    /// <returns>Return json as string</returns>
    public string getJson()
    {
        //TODO:Implement
        return null;
    }
}