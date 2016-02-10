using UnityEngine;
using System.Collections;

public class ReactionAI {
	private Enemy enemy;

	public ReactionAI(Enemy enemy) {
		this.enemy = enemy;
	}

	public Action React(GameEvent gameEvent) {
		Action action = null;
		if(gameEvent.type == GameEventType.EnemySpotted) {
			action = new ApproachAction(gameEvent.location);
		}
		return action;
	}
}
