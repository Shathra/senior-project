
using System.Collections.Generic;

/// <summary>
/// Data class for level statistics
/// </summary>
public class MLLevelStats {

    //Level Information
    public static float GuardianSpeed           = 1.0f;
    public static float GuardianTurnRate        = 1.0f;
    public static float GuardianWeaponDamage    = 1.0f;
    public static float GuardianMissileSpeed    = 1.0f;
    public static float GuardianAwarenessRange  = 1.0f;
    public static float GuardianWeaponAccuracy  = 1.0f;
    public static float GuardianReactionTime    = 1.0f;
    public static float GuardianAlertInterval   = 1.0f;
    public static float TurretAngularSpeed      = 1.0f;
    public static float TurretWeaponAccuracy    = 1.0f;
    public static float TurretMissleSpeed       = 1.0f;
    public static float CameraAngularSpeed      = 1.0f;
    public static float CameraAwarenessRange    = 1.0f;

    /*
     - Level time(sn cinsinden)
 - Level number
 - Kullanılan mermi sayısı
 - Number of guardians*
 - Zemin cinsi
 - Number of guardians*
 - Gadget'ların kullanma sayısı(frekans)
 - Öldürme stili
 - Number of guardians spawning interval
 - Difficulty(Sadece training datası için)
 - Guardian özellikleri
 	- Weapon damage
 	- Weapon missile spped
 	- Weapon accuracy
 	- Speed
 	- Awareness range
 	- Reaction time
 	- Alert süresi
 - Laser turret
 	- Period speed
 	- Weapon accuracy
 	- Weapon missle speed
    */

    

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