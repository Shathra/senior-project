using UnityEngine;
using System.Collections;

/// <summary>
/// Immobile unit which fires whenever it sees a player
/// </summary>
public class Turret : Enemy {
	private float rotationAmount = 85;
	private float range = 100;

	private LineRenderer line;
	private bool direction;
	private GameObject tracingObject;

	public override void Start() {
		line = GetComponent<LineRenderer>();
		direction = false;
		tracingObject = null;
	}

	public override void Update() {

	}

	void FixedUpdate() {
		if (tracingObject == null) {
			transform.eulerAngles = new Vector3(0, 0,
			Mathf.MoveTowards(transform.eulerAngles.z, direction ? 180 - rotationAmount : 180 + rotationAmount, Time.fixedDeltaTime * 40));
			if (Mathf.Abs(transform.eulerAngles.z - (180 - rotationAmount)) < 1)
				direction = false;
			else if (Mathf.Abs(transform.eulerAngles.z - (180 + rotationAmount)) < 1)
				direction = true;
		} else {
			Vector2 vec = Vector3.Normalize(tracingObject.GetComponent<Player>().midPoint - (Vector2)transform.parent.position);
			float angle = Mathf.Rad2Deg * Mathf.Asin(vec.x);
			transform.localEulerAngles = new Vector3(0, 0, Mathf.MoveTowards(0, angle, rotationAmount));
			Debug.Log(angle);
		}
		Vector3 origin = transform.parent.position + transform.rotation * Vector3.up * 0.25f;
		line.SetPosition(0, origin);
		line.SetPosition(1, transform.parent.position + transform.rotation * Vector3.up * range);
		RaycastHit2D obs = Physics2D.Raycast(origin, transform.rotation * Vector3.up, range, LayerMask.GetMask("Obstacle"));
		if (obs.collider != null)
			line.SetPosition(1, obs.point);
		RaycastHit2D hit = Physics2D.Raycast(origin, transform.rotation * Vector3.up,
			obs.collider != null ? Vector3.Distance(origin, obs.point) : range, LayerMask.GetMask("Player"));
		tracingObject = hit.collider != null ? hit.collider.gameObject : null;
		Color color = tracingObject ? Color.red : Color.green;
		line.SetColors(color, new Color(color.r, color.g, color.b, 0.5f));
	}
}
