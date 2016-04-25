using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Guardian : Enemy, ISpotable, IApproachable {
    public Vision vision { get; set; }
    public HeadSprite head { get; set; }
    public Eye eye { get; set; }

    public Gun gun { get; set; }

    public float totalUnconsciousTime { get; set; }

    protected float moveSpeed;
    protected float turnRate;

    private BoxCollider2D hitbox;
    private Rigidbody2D body;
    private Animator anim;
    private bool _onLadder;
    private Vector2 prevPos;
    private float unconsciousTime;
    private float angle;
	private Text unconsciousText;
	private Image unconsciousImage;
	private float bulletSpeed;

    public int patrolType;
    public Node patrolCoordinate1;
    public Node patrolCoordinate2;
    public float turnAroundTime;
    public GameObject bulletPrefab;

    private bool searchIsFinished;

    private bool _direction;
    public bool direction
    {
        get
        {
            return _direction;
        }
        set
        {
            _direction = value;
            transform.GetChild(0).transform.localScale = new Vector3((_direction ? -1 : 1) * Mathf.Abs(transform.GetChild(0).transform.localScale.x), transform.GetChild(0).transform.localScale.y, transform.GetChild(0).transform.localScale.z);
            transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.localScale = new Vector3((_direction ? -1 : 1) * Mathf.Abs(transform.GetChild(0).transform.localScale.x), transform.GetChild(0).transform.localScale.y, transform.GetChild(0).transform.localScale.z);
            transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.Rotate(new Vector3(0f, 0f, 180f));
        }
    }
    public bool onLadder {
        get {
            return _onLadder;
        }
        set {
            _onLadder = value;
            if (value)
                body.gravityScale = 0;
            else
                body.gravityScale = 1;
        }
    }
    public Vector2 topPoint {
        get {
            return new Vector2(hitbox.bounds.max.x - hitbox.size.x / 2, hitbox.bounds.max.y);
        }
    }

    public GameObject exclamationPrefab;

    private Rigidbody2D bullet;
    private float movement;

    public void Start() {
        head = transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<HeadSprite>();
        eye = transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Eye>();
        gun  = transform.GetChild(0).transform.GetChild(1).GetComponent<Gun>();
        this.moveSpeed = MLLevelStats.GetStat(LevelStat.GuardianSpeed);
        hitbox = GetComponent<BoxCollider2D>();
        body = GetComponent<Rigidbody2D>();
        onLadder = false;
        gameObject.layer = 11;                          //11 is Guardian Layer
        Idle(patrolType);
        vision = GetComponentInChildren<Vision>();
        vision.spotable = this;
		vision.gameObject.GetComponent<FOV2DEyes>().fovMaxDistance = MLLevelStats.GetStat(LevelStat.GuardianAwarenessRange); ;
        prevPos = transform.position;
        anim = GetComponent<Animator>();
        unconsciousTime = 0;
        totalUnconsciousTime = MLLevelStats.GetStat(LevelStat.GuardianUnconciousTime);
		unconsciousText = GetComponentInChildren<Text>();
		unconsciousImage = GetComponentInChildren<Image>();
		bulletSpeed = MLLevelStats.GetStat(LevelStat.GuardianMissileSpeed);
        //vision.GenerateVision(30, 10);
    }
    public void Approach(Vector2 target) {

        if (!onLadder && Physics2D.Raycast(topPoint, -Vector2.up, 0.01f, LayerMask.GetMask("Ladder"))) {
            onLadder = true;
        } else if (onLadder && !Physics2D.Raycast(topPoint, -Vector2.up, 0.01f, LayerMask.GetMask("Ladder"))) {
            onLadder = false;
        }
        anim.SetBool("Flying", onLadder);
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.x, onLadder ? target.y : transform.position.y), Time.deltaTime * moveSpeed);

        if (target.x - transform.position.x > 0) {
            if (direction) {
                direction = false;
            }
        } else if (target.x - transform.position.x < 0) {
            if (!direction) {
                direction = true;
            }
        }
    }

    public void Spot() {

        Debug.Log("SPOTTED");
        if (Player.instance != null && actionQueue.Peek().GetType() != typeof(FireAction)) {
            //todofirat Alert
            Exclamation ex = ((GameObject)Instantiate(exclamationPrefab,
                new Vector2(transform.position.x, transform.position.y + 1),
                Quaternion.identity)).GetComponent<Exclamation>();
            ex.transform.SetParent(gameObject.transform);
            ex.type = ExclamationType.Alerted;

            actionQueue.Insert(EventManager.Spot(new SpotEvent(this, Player.instance.midPoint)));
            return;
        }

        /*
		Rock rock = obj.GetComponent<Rock>();
		if (rock != null) {
            //todofirat Suspicious
            state.SetState(Status.Suspicious);
            
			if (rock.state != RockState.Ended)
				return;
			Exclamation ex = ((GameObject)Instantiate(exclamationPrefab,
				new Vector2(transform.position.x, transform.position.y),
				Quaternion.identity)).GetComponent<Exclamation>();
			ex.transform.SetParent(gameObject.transform);
			ex.type = ExclamationType.Suspicious;
            Suspicous();
            //Approach the noise
            Node nearestNode = AIController.GetNearestNode(rock.transform.position);
            actionQueue.Insert(new ApproachAction(transform.position, nearestNode.transform.position, -1));
            //
            return;
		}*/

    }

    public void SpotOut() {

        Debug.Log("SPOTT OUTED");
        actionQueue.Insert(EventManager.SpotOut(new SpotOutEvent(this, PlayerGhost.instance.transform.position)));
    }

    public void Fire(Vector2 target) {
        bullet = ((GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.identity)).GetComponent<Rigidbody2D>();
        angle = Mathf.Atan((target.y - transform.position.y) / (target.x - transform.position.x));
        //Debug.Log(angle);
        bullet.transform.Rotate(new Vector3(0, 0, angle * 180 / Mathf.PI));
        bullet.velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * -10 * (direction?1:-1) * bulletSpeed / 3;
    }

    public override void Update() {
        base.Update();
		unconsciousText.text = unconsciousTime > 0 ? "" + Mathf.Round(unconsciousTime) : "";
		unconsciousImage.fillAmount = unconsciousTime / totalUnconsciousTime;
		if (vision.playerInVision)
        {
            angle = Mathf.Atan((Player.instance.transform.position.y - transform.position.y) / (Player.instance.transform.position.x - transform.position.x));
            eye.transform.eulerAngles = new Vector3(0, 0, angle * 180 / Mathf.PI);
            gun.transform.eulerAngles = new Vector3(0, 0, angle * 180 / Mathf.PI);
            head.transform.eulerAngles = new Vector3(0, 0, -angle * 180 / Mathf.PI);
            if (direction)
            {
                head.transform.eulerAngles = new Vector3(0, 0, angle * 180 / Mathf.PI);
                gun.transform.eulerAngles = new Vector3(0, 0, -angle * 180 / Mathf.PI);
            }
        }
        else
        {
            eye.transform.eulerAngles = new Vector3(0, 0, 0);
            head.transform.eulerAngles = new Vector3(0, 0, 0);
            gun.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (unconsciousTime > 0) {
            unconsciousTime -= Time.deltaTime;
            return;
        } else
            anim.SetBool("Unconscious", false);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Unconscious") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("WakingUp")) {
            return;
        }
        //actionQueue.Insert(EventManager.Spot(new SpotEvent(this, new Vector2(-4.85f, 4.87f))));
        movement = Vector2.Distance(transform.position, prevPos);
        anim.SetFloat("Speed", movement > 0.01f ? 1.0f : 0.0f);
        prevPos = transform.position;
    }

    void Idle(int patrolType) {

        if (patrolType == 2) {
            actionQueue.Insert(new PatrolAction(patrolCoordinate1, patrolCoordinate2));     //walking patrol
        } else if (patrolType == 1) {
            actionQueue.Insert(new TurnAroundAction(turnAroundTime));                       //rotation patrol
        } else {
            //No rotation or no walking but may add some functions
        }

    }

    public void Knockout() {
        unconsciousTime = totalUnconsciousTime;
        anim.SetBool("Unconscious", true);
    }
}
