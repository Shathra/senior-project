using UnityEngine;
using System.Collections;

public class Guardian : Enemy, ISpotable, IApproachable {
	public Vision vision { get; set; }

	protected float moveSpeed;
	protected float turnRate;

	private BoxCollider2D hitbox;
	private Rigidbody2D body;
	private bool _onLadder;
	private bool _direction;

    public int patrolType;
    public Vector2 patrolCoordinate;
    public float turnAroundTime;

    private bool searchIsFinished;

    public bool direction {
		get {
			return _direction;
		}
		set {
			_direction = value;
			transform.localScale = new Vector3((direction ? 1 : -1) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
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

    public void Start()
    {
        this.moveSpeed = MLLevelStats.GetStat(LevelStat.GuardianSpeed);
        hitbox = GetComponent<BoxCollider2D>();
        body = GetComponent<Rigidbody2D>();
        onLadder = false;
        gameObject.layer = 11;                          //11 is Guardian Layer
        Idle(patrolType);
		vision = GetComponentInChildren<Vision>();
        vision.spotable = this;
		vision.GenerateVision(30, 10);
    }
	public void Approach(Vector2 target) {

		if (!onLadder && Physics2D.Raycast(topPoint, -Vector2.up, 0.01f, LayerMask.GetMask("Ladder"))) {
			onLadder = true;
		} else if (onLadder && !Physics2D.Raycast(topPoint, -Vector2.up, 0.01f, LayerMask.GetMask("Ladder"))) {
			onLadder = false;
		}
		transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.x, onLadder ? target.y : transform.position.y), Time.deltaTime * moveSpeed);
		if (target.x - transform.position.x > 0)
			direction = true;
		else if (target.x - transform.position.x < 0)
			direction = false;
	}

	public void Spot(GameObject obj) {

        Player player = obj.GetComponentInParent<Player>();
		if (player != null) {
            //todofirat Alert
            state.SetState(Status.Alert);
            //
            Exclamation ex = ((GameObject)Instantiate(exclamationPrefab,
				new Vector2(transform.position.x, transform.position.y + 1),
				Quaternion.identity)).GetComponent<Exclamation>();
			ex.transform.SetParent(gameObject.transform);
			ex.type = ExclamationType.Alerted;
            
			actionQueue.Insert(EventManager.Spot(new SpotEvent(this, player.midPoint)));
			return;
		}

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
		}
	}

	public void SpotOut(GameObject obj) {

	}

	public void Fire(Vector2 target) {

	}

	public override void Update() {
		base.Update();
		//actionQueue.Insert(EventManager.Spot(new SpotEvent(this, new Vector2(-4.85f, 4.87f))));
	}

    void Idle(int patrolType)
    {
        if (patrolType == 2)
        {
            actionQueue.Insert(new PatrolAction(transform.position, patrolCoordinate));     //walking patrol
        }
        else if (patrolType == 1)
        {
            actionQueue.Insert(new TurnAroundAction(turnAroundTime));                       //rotation patrol
        }
        else
        {
            //No rotation or no walking but may add some functions
        }
    }
    void Searching()
    {

        if (searchIsFinished)
        {
            state.SetState(Status.HasSeen);
        }          ///selam
    }   
    void HasSeen()
    {
        //later
    }
    void Suspicous()
    {
        //later
    }
    void Alert()
    {
        //sooner
    }
}
