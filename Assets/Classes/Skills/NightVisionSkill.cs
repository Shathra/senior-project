using UnityEngine;
using System.Collections;
using System;

public class NightVisionSkill : Skill {
	private GameObject nightVisionPrefab;

	private NightVision activeSkill;

	public NightVisionSkill() : base() {
		charges = 5000;
		name = "NightVision";
		description = "Reveals places not currently visible to the player.";
		nightVisionPrefab = Resources.Load<GameObject>("NightVision");
		activeSkill = GameObject.Instantiate(nightVisionPrefab).GetComponent<NightVision>();
		activeSkill.skill = this;
	}

	public override void Cast(Vector2 target) {
		activeSkill.running = !activeSkill.running;
	}
}
