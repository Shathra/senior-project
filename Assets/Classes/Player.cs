using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public static Player instance { get; private set; }
    private static float RUN_SOUND_DELAY = 0.3f;

	public GameObject rockPrefab;
    public GameObject playerGhost;
	public GameObject soundPrefab;

	public SkillSet skillSet { get; set; }
	public bool playerLock { get; set; }

	private Animator anim;
	private BoxCollider2D hitbox;
	private Rigidbody2D body;
	private bool _onLadder;
    private float runSoundInterval;

    public Vector2 lastPosition;       //MLLogger
    
    public bool carriesTarget;

    //Variables from update
    float crouching;
    bool ground;
    Vector2 hitboxBottom;
    RaycastHit2D[] result;
    float side;
    bool direction;
    float speed;
    float height;
    float crouchHeight;
    float crouchSpeed;
    Vector2 prev;
    float climbSpeed;
    //*****

    private bool onLadder {
		get {
			return _onLadder;
		}
		set {
			_onLadder = value;
			body.gravityScale = value ? 0.0f : 2.0f;
			if (value)
				body.velocity = new Vector2(body.velocity.x, 0);
		}
	}

    void Awake() {
		Player.instance = this;
		skillSet = new SkillSet();
	}

	void Start() {
        playerGhost = GameObject.Find("PlayerGhost");
        anim = GetComponent<Animator>();
		hitbox = GetComponent<BoxCollider2D>();
		body = GetComponent<Rigidbody2D>();
		onLadder = false;
		playerLock = false;
        runSoundInterval = RUN_SOUND_DELAY;
        lastPosition = transform.position;
	}

    void Update()
    {
        //MLLogger distance travelled
        MLLogger.IncrementStat(PlayStat.PlayerTravelDistance, Vector2.Distance(transform.position, lastPosition));
        //MLLogger Stuff End

        if (playerLock)
            return;
        crouching = anim.GetFloat("Crouching");
        ground = false;
        hitboxBottom = new Vector2(hitbox.bounds.min.x, hitbox.bounds.min.y);
        result = new RaycastHit2D[1];
        side = (hitbox.bounds.max.x - hitbox.bounds.min.x);

        //Debug.Log(side);
        if (Physics2D.BoxCast(bottomPoint, new Vector2(side - 0.01f, 0.01f), 0, -Vector2.up, 0.01f, LayerMask.GetMask("Obstacle")))
            ground = true;
        anim.SetBool("Ground", ground);

        if (Input.GetKeyDown(KeyCode.A) ^ Input.GetKeyDown(KeyCode.D))
            runSoundInterval = 0;
        if (Input.GetKey(KeyCode.A) ^ Input.GetKey(KeyCode.D))
        {
            if (ground && !onLadder && crouching < 0.5f)
            {
                runSoundInterval -= Time.deltaTime;
                if (runSoundInterval <= 0)
                {
                    Sound.GenerateSound(bottomPoint, 4);
                    runSoundInterval = RUN_SOUND_DELAY;
                }
            }
            direction = true;
            if (Input.GetKey(KeyCode.A))
                direction = false;
            speed = (5 - crouching * 2.5f) * (direction ? 1 : -1) * (onLadder ? 0.5f : 1.0f);
            transform.GetChild(0).transform.localScale = new Vector3((direction ? 1 : -1) * Mathf.Abs(transform.GetChild(0).transform.localScale.x), transform.GetChild(0).transform.localScale.y, transform.GetChild(0).localScale.z);
            //transform.position += speed * Vector3.right;
            body.velocity = new Vector2(speed, body.velocity.y);
            anim.SetFloat("Movement", 1);
        }
        else
        {
            body.velocity = new Vector2(0, body.velocity.y);
            anim.SetFloat("Movement", 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && ground)
            body.velocity += new Vector2(0, 7 - crouching * 1);
        if (!ground && !Input.GetKey(KeyCode.Space) && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x, 0);

        height = 1f;
        crouchHeight = 0.8f;
        crouchSpeed = 2.5f;

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (anim.GetFloat("Crouching") > 0)
            {
                if (!Physics2D.BoxCast(topPoint, new Vector2(side, 0.01f), 0, Vector2.up, height - crouchHeight, LayerMask.GetMask("Obstacle")))
                    anim.SetFloat("Crouching", 0);
            }
            else
                anim.SetFloat("Crouching", 1);
        }
        if (anim.GetFloat("Crouching") > 0)
        {
            //hitbox.offset = Vector2.MoveTowards(hitbox.offset, new Vector2(0, crouchHeight / 2), Time.deltaTime * crouchSpeed);
            hitbox.size = Vector2.MoveTowards(hitbox.size, new Vector2(hitbox.size.x, crouchHeight), Time.deltaTime * crouchSpeed);
        }
        else
        {
            //hitbox.offset = Vector2.MoveTowards(hitbox.offset, new Vector2(0, height / 2), Time.deltaTime * crouchSpeed);
            hitbox.size = Vector2.MoveTowards(hitbox.size, new Vector2(hitbox.size.x, height), Time.deltaTime * crouchSpeed);
        }

        if (onLadder)
        {
            anim.SetBool("LadderMoving", false);
            if (!Physics2D.Raycast(topPoint, -Vector2.up, 0.01f, LayerMask.GetMask("Ladder")))
                onLadder = false;
        }
        if (Input.GetKey(KeyCode.W) ^ Input.GetKey(KeyCode.S))
        {
            if (Physics2D.Raycast(topPoint, -Vector2.up, 0.01f, LayerMask.GetMask("Ladder")))
            {
                prev = transform.position;
                climbSpeed = 2 * Time.deltaTime;
                transform.position += climbSpeed * Vector3.up * ((Input.GetKey(KeyCode.W)) ? 1 : -1);
                anim.SetFloat("Crouching", 0.0f);
                onLadder = true;
                if (!Physics2D.Raycast(topPoint, -Vector2.up, 0.01f, LayerMask.GetMask("Ladder")))
                    transform.position = prev;
                anim.SetBool("LadderMoving", true);
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && ground && !onLadder)
            Takedown();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 target = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
            skillSet.Cast(target);
        }
        anim.SetFloat("VerticalSpeed", body.velocity.y);
        anim.SetBool("Ladder", onLadder);
    }

    void LateUpdate()
    {
        lastPosition = transform.position;
    }
    

    public void Takedown()
    {
        Vector2 origin = new Vector2((transform.localScale.x > 0 ? hitbox.bounds.max.x + 0.001f : hitbox.bounds.min.x - 0.001f), (hitbox.bounds.min.y + hitbox.bounds.max.y) / 2);
        float distance = 0.5f;
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : -Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, LayerMask.GetMask("Guardian", "Obstacle"));
        if (hit)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Guardian"))
            {
                playerLock = true;
                anim.SetTrigger("Takedown");
                anim.SetFloat("Movement", 0.0f);
                body.velocity = Vector2.zero;
                hit.collider.GetComponent<Guardian>().Knockout();
            }
        }
    }

    public void RemoveTakedownLock()
    {
        playerLock = false;
    }

    public Vector2[] keyPoints {
		get {
			Vector2[] points = new Vector2[5];
			points[0] = midPoint;
			points[1] = new Vector2(hitbox.bounds.min.x, hitbox.bounds.min.y);
			points[2] = new Vector2(hitbox.bounds.min.x, hitbox.bounds.max.y);
			points[3] = new Vector2(hitbox.bounds.max.x, hitbox.bounds.max.y);
			points[4] = new Vector2(hitbox.bounds.max.x, hitbox.bounds.min.y);
			return points;
		}
	}

	public Vector2 topPoint {
		get {
			return new Vector2(hitbox.bounds.max.x - hitbox.size.x / 2, hitbox.bounds.max.y);
		}
	}

	public Vector2 midPoint {
		get {
			return new Vector2(hitbox.bounds.min.x + hitbox.size.x / 2, hitbox.bounds.min.y + hitbox.size.y / 2);
		}
	}

	public Vector2 bottomPoint {
		get {
			return new Vector2(hitbox.bounds.min.x + hitbox.size.x / 2, hitbox.bounds.min.y);
		}
	}
    void OnCollisionEnter2D(Collision2D col) {

        if (col.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            Debug.Log("Sound!");
            Sound.GenerateSound(bottomPoint, body.velocity.y/2);
        }
    }

	public void Die() {
		anim.SetTrigger("Die");
		playerLock = true;
	}
}
