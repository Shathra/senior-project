using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ApproachAction : Action {
    private Vector2 target;
    private List<Node> path;

    public ApproachAction(Vector2 source, Vector2 target) {
        this.target = target;
        path = AIController.GetPath(source, target);
    }

    public override void Execute(Enemy enemy) {
        IApproachable movingObj = (IApproachable)enemy;
        
        if( path.Count == 0) {
            done = true;
            return;
        }

        Vector2 nextNode = path[0].transform.position;
        movingObj.Approach(nextNode);
		if(Vector2.Distance(enemy.transform.position, nextNode) < 0.1f) {

			AIController.GetPath(enemy.transform.position, target);
			path.RemoveAt(0);
		}
    }
}
