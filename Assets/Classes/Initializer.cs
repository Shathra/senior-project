using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;


/// <summary>
/// Initializer class which is responsible for initializeing static classes
/// </summary>
class Initializer : MonoBehaviour {
    string strCmdText;
    public void Awake() {

        GameController.Init();
        MLLogger.Init();
        MLLevelStats.Init();
        
        strCmdText = "/C cd DifficultyEstimator & python estimator.py";
        //Process.Start("CMD.exe", strCmdText);

        MLCommunicator.Init();

        MLCommunicator.predictDifficulty();
    }

    public void OnApplicationQuit() {

        MLCommunicator.Close();
    }
}
