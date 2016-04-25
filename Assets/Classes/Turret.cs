using UnityEngine;
using System.Collections;

/// <summary>
/// Immobile unit which fires whenever it sees a player
/// </summary>
public class Turret : Enemy {
	public GameObject bulletPrefab;

	private const float ROTATION_AMOUNT = 85;
	private const float RANGE = 100;

	public float fireRate { get; set; }
	public float delay { get; set; }
	public float rotationSpeed { get; set; }
	public float bulletSpeed { get; set; }

	private float shoot;
	private float remainingDelay;

	private LineRenderer line;
	private bool direction;
	private GameObject tracingObject;
    //From functions
    Vector2 vec;
    float angle;
    Rigidbody2D bullet;
    RaycastHit2D obs;
    RaycastHit2D hit;
    Color color;
    void Start() {
		fireRate = MLLevelStats.GetStat(LevelStat.TurretFireRate);
		delay = MLLevelStats.GetStat(LevelStat.TurretFireDelay);
		rotationSpeed = MLLevelStats.GetStat(LevelStat.TurretAngularSpeed);
		bulletSpeed = MLLevelStats.GetStat(LevelStat.TurretMissileSpeed);
		line = GetComponent<LineRenderer>();
		direction = false;
		tracingObject = null;
		shoot = 0;
		remainingDelay = delay;
	}
    
	public Vector2 tip {
		get {
			return transform.parent.position + transform.rotation * Vector3.up * 0.25f;
		}
	}

	void FixedUpdate() {
		if (tracingObject == null) {
			transform.eulerAngles = new Vector3(0, 0,
			Mathf.MoveTowards(transform.eulerAngles.z, direction ? 180 - ROTATION_AMOUNT : 180 + ROTATION_AMOUNT, Time.fixedDeltaTime * rotationSpeed));
			if (Mathf.Abs(transform.eulerAngles.z - (180 - ROTATION_AMOUNT)) < 1)
				direction = false;
			else if (Mathf.Abs(transform.eulerAngles.z - (180 + ROTATION_AMOUNT)) < 1)
				direction = true;
			remainingDelay = delay;
		} else {
			vec = Vector3.Normalize(tracingObject.GetComponent<Player>().midPoint - (Vector2)transform.parent.position);
			angle = Mathf.Rad2Deg * Mathf.Asin(vec.x);
			transform.localEulerAngles = new Vector3(0, 0, Mathf.MoveTowards(0, angle, ROTATION_AMOUNT));
			if (shoot <= 0 && remainingDelay <= 0) {
				bullet = ((GameObject)Instantiate(bulletPrefab, tip, Quaternion.identity)).GetComponent<Rigidbody2D>();
                bullet.transform.Rotate(new Vector3(0, 0, angle+90));
                bullet.velocity = transform.rotation * Vector3.up * 20 * bulletSpeed;
				shoot = 1 / fireRate;
			} else {
				shoot -= Time.deltaTime;
				remainingDelay -= Time.deltaTime;
			}
		}
		line.SetPosition(0, tip);
		line.SetPosition(1, transform.parent.position + transform.rotation * Vector3.up * RANGE);
		obs = Physics2D.Raycast(tip, transform.rotation * Vector3.up, RANGE, LayerMask.GetMask("Obstacle"));
		if (obs.collider != null)
			line.SetPosition(1, obs.point);
		hit = Physics2D.Raycast(tip, transform.rotation * Vector3.up,
			obs.collider != null ? Vector3.Distance(tip, obs.point) : RANGE, LayerMask.GetMask("Player"));
		tracingObject = hit.collider != null ? hit.collider.gameObject : null;
		color = tracingObject ? Color.red : Color.green;
		line.SetColors(color, new Color(color.r, color.g, color.b, 0.5f));
	}
}
