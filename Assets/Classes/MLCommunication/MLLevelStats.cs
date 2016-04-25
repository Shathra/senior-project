
using System;
using System.Collections.Generic;

public enum LevelStat {

    GuardianSpeed, GuardianMissileSpeed, GuardianWeaponFireRate,
    GuardianAwarenessRange, GuardianReactionTime, GuardianUnconciousTime, TurretAngularSpeed,
    TurretMissileSpeed, TurretFireRate, TurretFireDelay, CameraAngularSpeed, CameraAwarenessRange,
    HackingTime, ShurikenLimit
}

public enum LevetStatMin {

    GuardianSpeed, GuardianMissileSpeed, GuardianWeaponFireRate,
    GuardianAwarenessRange, GuardianReactionTime, GuardianUnconciousTime, TurretAngularSpeed,
    TurretMissileSpeed, TurretFireRate, TurretFireDelay, CameraAngularSpeed, CameraAwarenessRange,
    HackingTime, ShurikenLimit
}

public enum LevelStatMax {

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
    private static Dictionary<LevetStatMin, float> minLevelStats;
    private static Dictionary<LevelStatMax, float> maxLevelStats;

    protected static float GuardianSpeedMin = 0.8f;
    protected static float GuardianMissileSpeedMin = 0.33f;
    protected static float GuardianWeaponFireRateMin = 1;
    protected static float GuardianAwarenessRangeMin = 4;
    protected static float GuardianReactionTimeMin = 0;
    protected static float GuardianUnconciousTimeMin = 3;
    protected static float TurretAngularSpeedMin = 20;
    protected static float TurretMissileSpeedMin = 0.33f;
    protected static float TurretFireRateMin = 0.5f;
    protected static float TurretFireDelayMin = 0;
    protected static float CameraAngularSpeedMin = 20;
    protected static float CameraAwarenessRangeMin = 4;
    protected static float HackingTimeMin = 1;
    protected static float ShurikenLimitMin = 1;
	protected static float LevelTimeMin = 30;
    protected static float NumberOfTrialsMin = 1;
    protected static float PlayerTravelDistanceMin = 10;

    protected static float GuardianSpeedMax = 1.5f;
    protected static float GuardianMissileSpeedMax = 1;
    protected static float GuardianWeaponFireRateMax = 4;
    protected static float GuardianAwarenessRangeMax = 7;
    protected static float GuardianReactionTimeMax = 1;
    protected static float GuardianUnconciousTimeMax = 9;
    protected static float TurretAngularSpeedMax = 60;
    protected static float TurretMissileSpeedMax = 1;
    protected static float TurretFireRateMax = 1.5f;
    protected static float TurretFireDelayMax = 1;
    protected static float CameraAngularSpeedMax = 60;
    protected static float CameraAwarenessRangeMax = 7;
    protected static float HackingTimeMax = 5;
    protected static float ShurikenLimitMax = 9;
    protected static float LevelTimeMax = 300;
    protected static float NumberOfTrialsMax = 10;
    protected static float PlayerTravelDistanceMax = 100;

    public static void Init() {

        levelStats = new Dictionary<LevelStat, float>();
        minLevelStats = new Dictionary<LevetStatMin, float>();
        maxLevelStats = new Dictionary<LevelStatMax, float>();


        foreach (LevelStat val in Enum.GetValues(typeof(LevelStat))) {
            levelStats.Add(val, Constants.INVALID_STAT);
        }
        
        foreach (LevetStatMin val in Enum.GetValues(typeof(LevetStatMin))) {
            minLevelStats.Add(val, Constants.INVALID_STAT);
        }

        foreach (LevelStatMax val in Enum.GetValues(typeof(LevelStatMax))) {
            maxLevelStats.Add(val, Constants.INVALID_STAT);
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

        minLevelStats[LevetStatMin.GuardianSpeed] = 0.8f;
        minLevelStats[LevetStatMin.GuardianMissileSpeed] = 0.33f;
        minLevelStats[LevetStatMin.GuardianWeaponFireRate] = 1;
        minLevelStats[LevetStatMin.GuardianAwarenessRange] = 4;
        minLevelStats[LevetStatMin.GuardianReactionTime] = 0;
        minLevelStats[LevetStatMin.GuardianUnconciousTime] = 3;
        minLevelStats[LevetStatMin.TurretAngularSpeed] = 20;
        minLevelStats[LevetStatMin.TurretMissileSpeed] = 0.33f;
        minLevelStats[LevetStatMin.TurretFireRate] = 0.5f;
        minLevelStats[LevetStatMin.TurretFireDelay] = 0;
        minLevelStats[LevetStatMin.CameraAngularSpeed] = 20;
        minLevelStats[LevetStatMin.CameraAwarenessRange] = 4;
        minLevelStats[LevetStatMin.HackingTime] = 1;
        minLevelStats[LevetStatMin.ShurikenLimit] = 1;

        maxLevelStats[LevelStatMax.GuardianSpeed] = 1.5f;
        maxLevelStats[LevelStatMax.GuardianMissileSpeed] = 1;
        maxLevelStats[LevelStatMax.GuardianWeaponFireRate] = 4;
        maxLevelStats[LevelStatMax.GuardianAwarenessRange] = 7;
        maxLevelStats[LevelStatMax.GuardianReactionTime] = 1;
        maxLevelStats[LevelStatMax.GuardianUnconciousTime] = 9;
        maxLevelStats[LevelStatMax.TurretAngularSpeed] = 60;
        maxLevelStats[LevelStatMax.TurretMissileSpeed] = 1;
        maxLevelStats[LevelStatMax.TurretFireRate] = 1.5f;
        maxLevelStats[LevelStatMax.TurretFireDelay] = 1;
        maxLevelStats[LevelStatMax.CameraAngularSpeed] = 60;
        maxLevelStats[LevelStatMax.CameraAwarenessRange] = 7;
        maxLevelStats[LevelStatMax.HackingTime] = 5;
        maxLevelStats[LevelStatMax.ShurikenLimit] = 9;
    }

    public static void SetStat(LevelStat stat, float value) {

        levelStats[stat] = value;
    }

    public static float GetStat(LevelStat stat) {
		if (levelStats == null)
			Init();
        return levelStats[stat];
    }

    public static Dictionary<LevelStat, float> GetStats() {

        return levelStats;
    }

    public static float GetMinStat(LevetStatMin stat) {

        return minLevelStats[stat];
    }

    public static float GetMaxStat(LevelStatMax stat) {

        return maxLevelStats[stat];
    }
}