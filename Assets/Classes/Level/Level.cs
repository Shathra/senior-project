using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;

public class Level : MonoBehaviour {

    protected Stopwatch timer;
    protected int levelId;

    public void Start() {

        timer = new Stopwatch();
        timer.Start();
        initLevel();
    }

    public long GetElapsedTime() {

        return timer.ElapsedMilliseconds / 1000;
    }

    protected virtual void initLevel() {

    }
}
