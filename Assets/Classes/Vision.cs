using UnityEngine;
using System.Collections;

public class Vision : MonoBehaviour {
	public ISpotable spotable { get; set; }
	private bool playerCollidingVision;
	private bool _playerInVision;
	public PolygonCollider2D visionCollider;
	private MeshFilter visionMesh;
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
				spotable.SpotOut();
			} else if(value && !playerInVision){
                PlayerGhost.instance.transform.GetChild(0).gameObject.SetActive(false);
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
	}

	void FixedUpdate() {
		if (playerCollidingVision) {
			bool hit = false;
			foreach (Vector2 point in Player.instance.keyPoints) {
				RaycastHit2D rcHit = Physics2D.Raycast(transform.position,
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

	void Update() {
	}

	/*public void GenerateVision(float angle, float range) {
		Vector2[] points = new Vector2[3];
		points[0] = Vector2.zero;
		float x = Mathf.Sin((angle / 2) * Mathf.Deg2Rad) * range;
		float y = Mathf.Cos((angle / 2) * Mathf.Deg2Rad) * range;
		points[1] = new Vector2(-x, y);
		points[2] = new Vector2(x, y);
		//visionCollider.points = points;
		Mesh mesh = new Mesh();
		mesh.vertices = new Vector3[] { (Vector3)points[0], (Vector3)points[1], (Vector3)points[2] };
		mesh.triangles = new int[] { 0, 1, 2 };
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		visionMesh.mesh = mesh;
	}*/
}
