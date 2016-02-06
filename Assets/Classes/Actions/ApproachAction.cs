using UnityEngine;
using System.Collections;

public class ApproachAction : Action {
    private Vector2 target;

    public ApproachAction(Vector2 target) {
        this.target = target;
    }

    public override void Execute(Enemy enemy) {
        IApproachable movingObj = (IApproachable)enemy;
        movingObj.Approach(target);
		if(Vector2.Distance(enemy.transform.position, new Vector2(target.x, enemy.transform.position.y)) < 0.01f) {
			done = true;
		}
    }
}
