using UnityEngine;
using System.Collections;
using System;

public class NightVisionSkill : Skill {
	private GameObject nightVisionPrefab;

	public NightVisionSkill() : base() {
		charges = 1;
		name = "NightVision";
		description = "Reveals places not currently visible to the player.";
		nightVisionPrefab = Resources.Load<GameObject>("NightVision");
	}

	public override void Cast(Vector2 target) {
		GameObject nightVision = GameObject.Instantiate(nightVisionPrefab);
		nightVision.transform.parent = UIController.canvas.transform;
		nightVision.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
	}
}
