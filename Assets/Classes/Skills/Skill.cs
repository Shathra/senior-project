using UnityEngine;
using System.Collections;

public abstract class Skill {
	public int charges { get; set; }
	public string name { get; set; }
	public string description { get; set; }

	protected Player player;

	public Skill() {
		player = Player.instance;
	}

	public abstract void Cast(Vector2 target);
}
