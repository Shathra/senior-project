using UnityEngine;
using System.Collections;

public class SearchAction : Action {
    protected Node suspectedPoint;

    public SearchAction(PlayerGhost playerGhost)
        : base(Action.PRIORITY_SEARCH) {
        suspectedPoint = AIController.GetNearestNode(playerGhost.transform.position);
    }
    public SearchAction(Player player)
        : base(Action.PRIORITY_SEARCH) {
        suspectedPoint = AIController.GetNearestNode(player.transform.position);
    }
    public SearchAction(Vector2 pos)
        : base(Action.PRIORITY_SEARCH)
    {
        suspectedPoint = AIController.GetNearestNode(pos);
    }


    public override void Execute(Enemy enemy) {
        enemy.actionQueue.Insert(new ApproachAction(enemy.transform.position, suspectedPoint.transform.position, Action.PRIORITY_SEARCH_APPROACH));
        enemy.actionQueue.Insert(new TurnAroundAction(0.5f,2,Action.PRIORITY_SEARCH));
        enemy.actionQueue.Remove(this);
        
    }
}
