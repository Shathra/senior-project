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

    private static bool mlInitialized = false;
    public void Awake() {

        if (!mlInitialized) {
            GameController.Init();
            MLLogger.Init();
            MLLevelStats.Init();
            //MLController.SetLevelStats( 50, MLConfig.DefaultDifficulty);

            strCmdText = "/C cd DifficultyEstimator & python estimator.py";
			//Process.Start("CMD.exe", strCmdText);

			MLCommunicator.Init();
            mlInitialized = true;

            MLController.AdjustDifficulty();
        }
        //MLCommunicator.predictDifficulty();
    }

    public void OnApplicationQuit() {

        MLCommunicator.Close();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.J)) {
             MLController.AdjustDifficulty();
        }
    }
}
