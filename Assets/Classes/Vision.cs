using UnityEngine;
using System.Collections;

public class Vision : MonoBehaviour {
	public ISpotable spotable { get; set; }
	private bool playerCollidingVision;
	private bool _playerInVision;
	public bool playerInVision {
		get {
			return _playerInVision;
		}
		set {
			if (value)
				AIController.lastKnownPosition = Player.instance.midPoint;
			if (!value && playerInVision) {
				PlayerGhost.instance.transform.position = Player.instance.midPoint;
				PlayerGhost.instance.transform.GetChild(0).gameObject.SetActive(true);
			}
			_playerInVision = value;
		}
	}

	void Start() {
		_playerInVision = false;
		playerCollidingVision = false;
	}

	void FixedUpdate() {
		if (playerCollidingVision) {
			bool hit = false;
			foreach (Vector2 point in Player.instance.keyPoints) {
				if (Physics2D.Raycast(transform.position,
					point - (Vector2)transform.position, 100,
					LayerMask.GetMask("Player", "Obstacle"))
						.collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
					hit = true;
					break;
				}
			}
			if (hit)
				playerInVision = true;
			else
				playerInVision = false;
		} else
			playerInVision = false;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
			playerCollidingVision = true;
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
			playerCollidingVision = false;
	}

	void Update() {
		if (playerInVision)
			spotable.Spot(Player.instance.gameObject);
	}
}
