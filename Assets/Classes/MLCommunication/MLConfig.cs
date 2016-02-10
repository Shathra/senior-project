using System.Collections;

/// <summary>
/// Contains config information needed to communicate with ML module.
/// </summary>
public class MLConfig {

    protected static readonly bool isConfigureFromFile = false;
    protected static readonly string configFilePath = "";

    public static string MLInputPath { get; protected set; }

    public static string MLOutputPath { get; protected set; }

    public static void ConfigureFromFile()
    {
        //TODO:Implement
    }
}
