using UnityEngine;
using System.Collections;

public class Vision : MonoBehaviour {
	public ISpotable spotable { get; set; }
	private bool playerCollidingVision;
	private bool _playerInVision;
	public PolygonCollider2D visionCollider;
	private MeshFilter visionMesh;
    //From functions
    bool hit;
    RaycastHit2D rcHit;
	public FOV2DEyes eyes { get; set; }
    public bool playerInVision {
		get {
			return _playerInVision;
		}
		set {
			if (value)
				AIController.lastKnownPosition = Player.instance.midPoint;
			if (!value && playerInVision) {
                PlayerGhost.instance.SetActive(true);
				spotable.SpotOut();
            } else if (value && !playerInVision) {
                PlayerGhost.instance.SetActive(false);
                spotable.Spot();
            }
			_playerInVision = value;
		}
	}

	void Awake() {
		visionCollider = GetComponent<PolygonCollider2D>();
		visionMesh = GetComponent<MeshFilter>();
	}

	void Start() {
		_playerInVision = false;
		playerCollidingVision = false;
		eyes = GetComponent<FOV2DEyes>();
	}

	void FixedUpdate() {
		if (playerCollidingVision) {
			hit = false;
			foreach (Vector2 point in Player.instance.keyPoints) {
				rcHit = Physics2D.Raycast(transform.position,
					point - (Vector2)transform.position, 100,
					LayerMask.GetMask("Player", "Obstacle"));
				if (rcHit) {
					if (rcHit.collider != null) {
						if (rcHit.collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
							hit = true;
							break;
						}
					}
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
}
