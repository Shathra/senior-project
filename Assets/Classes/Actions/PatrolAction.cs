using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Specifies action which source patrol between two points
/// </summary>
class PatrolAction : Action {

    protected Node point1;
    protected Node point2;
    protected bool toPoint1;
    
    public PatrolAction(Node point1, Node point2) {

        this.point1 = point1;
        this.point2 = point2;
        toPoint1 = true;
    }

    public PatrolAction( Vector2 point1, Vector2 point2) {

        Node node1 = AIController.GetNearestNode(point1);
        Node node2 = AIController.GetNearestNode(point2);
        this.point1 = node1;
        this.point2 = node2;
        toPoint1 = true;
    }

    public override void Execute(Enemy enemy) {

        IApproachable movingObj = (IApproachable)enemy;
        if (toPoint1) {
            enemy.actionQueue.Insert(new ApproachAction(enemy.transform.position, point1.transform.position, Action.PRIORITY_DEFAULT));
        }
        else {
            enemy.actionQueue.Insert(new ApproachAction(enemy.transform.position, point2.transform.position, Action.PRIORITY_DEFAULT));
        }
        toPoint1 = !toPoint1;
    }
}
