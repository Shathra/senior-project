using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class LevelEz : Level {

    protected override void initLevel() {
        base.initLevel();
        List<Guardian> guards = AIController.GetGuardians();
        levelId = Constants.LEVEL_ID_EASY;

        List<Node> enemyPositions = new List<Node>();
        foreach( Enemy enemy in guards) {
            enemyPositions.Add(AIController.GetNearestNode(enemy.transform.position));
        }
        List<List<Node>> listPath = Graph.SearchGraph( AIController.GetGraph(), enemyPositions);

        for (int i = 0; i < guards.Count; i++) {

            Node prev = AIController.GetNearestNode(guards[i].transform.position);
            int am = 20;
            foreach (Node node in listPath[i]) {

                Debug.Log(node);
                guards[i].actionQueue.Insert(new ApproachAction(prev.transform.position, node.transform.position, am));
                prev = node;
                am++;
            }
        }
    }
}