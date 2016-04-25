
using System;
using System.Collections.Generic;

public enum LevelStat {

    GuardianSpeed, GuardianMissileSpeed, GuardianWeaponFireRate,
    GuardianAwarenessRange, GuardianReactionTime, GuardianUnconciousTime, TurretAngularSpeed,
    TurretMissileSpeed, TurretFireRate, TurretFireDelay, CameraAngularSpeed, CameraAwarenessRange,
    HackingTime, ShurikenLimit
}

/// <summary>
/// Data class for level statistics
/// </summary>
public class MLLevelStats {

    public static Dictionary<LevelStat, float> levelStats;

    public static void Init() {

        levelStats = new Dictionary<LevelStat, float>();
        foreach (LevelStat val in Enum.GetValues(typeof(LevelStat))) {
            levelStats.Add(val, Constants.INVALID_STAT);
        }

        levelStats[LevelStat.GuardianSpeed] = 1.0f;
        levelStats[LevelStat.GuardianMissileSpeed] = 1.0f;
        levelStats[LevelStat.GuardianWeaponFireRate] = 2.0f;
        levelStats[LevelStat.GuardianAwarenessRange] = 1.0f;
        levelStats[LevelStat.GuardianReactionTime] = 1.0f;
        levelStats[LevelStat.GuardianUnconciousTime] = 1.0f;

        levelStats[LevelStat.TurretAngularSpeed] = 1.0f;
        levelStats[LevelStat.TurretMissileSpeed] = 1.0f;
        levelStats[LevelStat.TurretFireRate] = 1.0f;
        levelStats[LevelStat.TurretFireDelay] = 1.0f;

        levelStats[LevelStat.HackingTime] = 1.0f;
        levelStats[LevelStat.ShurikenLimit] = 1.0f;

        levelStats[LevelStat.CameraAngularSpeed] = 1.0f;
        levelStats[LevelStat.CameraAwarenessRange] = 1.0f;
    }

    public static void SetStat(LevelStat stat, float value) {

        levelStats[stat] = value;
    }

    public static float GetStat(LevelStat stat) {

        return levelStats[stat];
    }

    public static Dictionary<LevelStat, float> GetStats() {

        return levelStats;
    }
}