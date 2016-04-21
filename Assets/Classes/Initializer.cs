using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;


/// <summary>
/// Initializer class which is responsible for initializeing static classes
/// </summary>
class Initializer : MonoBehaviour {

    public void Awake() {

        GameController.Init();
        MLLogger.Init();
        MLLevelStats.Init();

        string strCmdText;
        strCmdText = "/C cd DifficultyEstimator & python estimator.py & set /p DUMMY=Hit ENTER to continue...";
        System.Diagnostics.Process.Start("CMD.exe", strCmdText);

        MLCommunicator.Init();

        MLCommunicator.predictDifficulty();
    }

    public void OnApplicationQuit() {

        MLCommunicator.Close();
    }
}
