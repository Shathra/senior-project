using System.Collections;
using UnityEngine;

/// <summary>
/// Contains config information needed to communicate with ML module.
/// </summary>
public class MLConfig {

    protected static readonly bool isConfigureFromFile = false;
    protected static readonly string configFilePath = "";
    public static readonly int MLPort = 5005;
    public static readonly float DefaultDifficulty = 50.0f;

    public static string MLInputPath { get; protected set; }

    public static string MLOutputPath = Application.persistentDataPath;

    public static void ConfigureFromFile()
    {
        
    }
}
