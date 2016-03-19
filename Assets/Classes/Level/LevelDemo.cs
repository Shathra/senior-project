using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class LevelDemo : Level {

    protected override void initLevel() {
        base.initLevel();
        List<Enemy> enemies = AIController.GetEnemies();
        enemies[0].actionQueue.Insert(new PatrolAction(enemies[0].transform.position, new Vector2(-2.8f, -1)));
        levelId = 1;
    }
}