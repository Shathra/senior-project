
using System;
using System.Collections.Generic;

public enum LevelStat {

    GuardianSpeed, GuardianTurnRate, GuardianWeaponDamage, GuardianMissileSpeed,
    GuardianAwarenessRange, GuardianWeaponAccuracy, GuardianReactionTime, GuardianAlertInterval, TurretAngularSpeed, TurretWeaponAccuracy,
    TurretMissleSpeed, CameraAngularSpeed, CameraAwarenessRange
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
        levelStats[LevelStat.GuardianTurnRate] = 1.0f;
        levelStats[LevelStat.GuardianWeaponDamage] = 1.0f;
        levelStats[LevelStat.GuardianMissileSpeed] = 1.0f;
        levelStats[LevelStat.GuardianAwarenessRange] = 1.0f;
        levelStats[LevelStat.GuardianWeaponAccuracy] = 1.0f;
        levelStats[LevelStat.GuardianReactionTime] = 1.0f;
        levelStats[LevelStat.GuardianAlertInterval] = 1.0f;
        levelStats[LevelStat.TurretAngularSpeed] = 1.0f;
        levelStats[LevelStat.TurretWeaponAccuracy] = 1.0f;
        levelStats[LevelStat.TurretMissleSpeed] = 1.0f;
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