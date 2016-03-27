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
        levelId = Constants.INVALID_LEVEL;
        initLevel();
    }

    public long GetElapsedTime() {

        timer.Stop();
        return timer.ElapsedMilliseconds / 1000;
    }

    protected virtual void initLevel() {

    }

    public int GetId() {

        return levelId;
    }

    /// <summary>
    /// Yaz bunu güzel laf
    /// </summary>
    public void GiveSearchOrderToAllGuardians() {

        List<Guardian> guards = AIController.GetGuardians();

        List<Node> enemyPositions = new List<Node>();
        foreach (Enemy enemy in guards) {
            enemyPositions.Add(AIController.GetNearestNode(enemy.transform.position));
        }
        List<List<Node>> listPath = Graph.SearchGraph(AIController.GetGraph(), enemyPositions);
        List<ActionBundle> enemyActions = new List<ActionBundle>();

        for (int i = 0; i < guards.Count; i++) {

            enemyActions[i] = new ActionBundle();
            Node prev = AIController.GetNearestNode(guards[i].transform.position);
            foreach (Node node in listPath[i]) {

                enemyActions[i].Add(new ApproachAction(prev.transform.position, node.transform.position));
                //guards[i].actionQueue.Insert(new ApproachAction(prev.transform.position, node.transform.position, am));
                prev = node;
            }

            guards[i].actionQueue.Insert(enemyActions[i]);
        }
    }
}
