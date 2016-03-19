using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


/// <summary>
/// Initializer class which is responsible for initializeing static classes
/// </summary>
class Initializer : MonoBehaviour {

    public void Start() {

        GameController.Init();
        MLLogger.Init();

    }
}
