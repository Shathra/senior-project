using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class LevelEz : Level {

    protected override void initLevel() {
        base.initLevel();

        List<Guardian> guards = AIController.GetGuardians();
        Graph graph = AIController.GetGraph();
        //guards[0].actionQueue.Insert(new PatrolAction(new Vector2(-10.25f, 5.0f), new Vector2(-1.1f, 5.0f)));

        levelId = Constants.LEVEL_ID_EASY;
    }
}