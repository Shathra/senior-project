using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class LevelDemo : Level {

    protected override void initLevel() {
        base.initLevel();
        List<Guardian> guards = AIController.GetGuardians();
        guards[0].actionQueue.Insert(new PatrolAction(guards[0].transform.position, new Vector2(-2.8f, -1)));
        levelId = 1;
    }
}