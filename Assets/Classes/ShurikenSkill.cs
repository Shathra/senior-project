using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ShurikenSkill : Skill {
	private GameObject shurikenPrefab;

	public ShurikenSkill() : base() {
		charges = 4;
		name = "Shuriken";
		description = "Despite the implication of its name, " +
			"Prometheus uses this seemingly uncivilized weapon " +
			"to distract his enemies.";
		shurikenPrefab = Resources.Load<GameObject>("Shuriken");
	}

	public override void Cast(Vector2 target) {
		Vector2 direction = target - player.midPoint;
		Rigidbody2D shuriken = ((GameObject)MonoBehaviour.Instantiate(shurikenPrefab,
			player.midPoint, Quaternion.identity)).GetComponent<Rigidbody2D>();
		shuriken.AddForce(direction.normalized * 80);
	}
}
