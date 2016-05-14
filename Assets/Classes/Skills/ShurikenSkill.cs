using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ShurikenSkill : Skill {

    private GameObject shurikenPrefab;
    Vector2 direction;
    Rigidbody2D shuriken;

    public ShurikenSkill() : base() {
		charges = Mathf.RoundToInt(MLLevelStats.GetStat(LevelStat.ShurikenLimit));
		name = "Shuriken";
		description = "Despite the implication of its name, " +
			"Prometheus uses this seemingly uncivilized weapon " +
			"to distract his enemies.";
		shurikenPrefab = Resources.Load<GameObject>("Shuriken");
	}

	public override void Cast(Vector2 target) {
		direction = target - player.midPoint;
		shuriken = ((GameObject)MonoBehaviour.Instantiate(shurikenPrefab,
			player.midPoint, Quaternion.identity)).GetComponent<Rigidbody2D>();
		shuriken.AddForce(direction.normalized * 80);
        charges++; //Unlimited for demo
	}
}
