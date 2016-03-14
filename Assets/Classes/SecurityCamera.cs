using UnityEngine;
using System.Collections;

/// <summary>
/// Immobile unit which alerts other enemy in a range when it sees a player
/// </summary>
public class SecurityCamera : Enemy {
	private const float ROTATION_AMOUNT = 85;

	private bool direction;

	void Start() {
		direction = false;
	}

	void FixedUpdate() {
		transform.eulerAngles = new Vector3(0, 0,
			Mathf.MoveTowards(transform.eulerAngles.z, direction ? 180 - ROTATION_AMOUNT : 180 + ROTATION_AMOUNT, Time.fixedDeltaTime * 40));
		if (Mathf.Abs(transform.eulerAngles.z - (180 - ROTATION_AMOUNT)) < 1)
			direction = false;
		else if (Mathf.Abs(transform.eulerAngles.z - (180 + ROTATION_AMOUNT)) < 1)
			direction = true;
	}
}
