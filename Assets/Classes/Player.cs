﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public GameObject rockPrefab;

	private Animator anim;
	private BoxCollider2D hitbox;
	private Rigidbody2D body;
	private bool _onLadder;
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

	void Start() {
		anim = GetComponent<Animator>();
		hitbox = GetComponent<BoxCollider2D>();
		body = GetComponent<Rigidbody2D>();
		onLadder = false;
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

	void Update() {
		float crouching = anim.GetFloat("Crouching");
		bool ground = false;
		Vector2 hitboxBottom = new Vector2(hitbox.bounds.min.x, hitbox.bounds.min.y);
		RaycastHit2D[] result = new RaycastHit2D[1];
		if (Physics2D.LinecastNonAlloc(hitboxBottom, hitboxBottom - Vector2.up * 0.02f, result, LayerMask.GetMask("Obstacle")) > 0)
			ground = true;
		else {
			hitboxBottom += Vector2.right * hitbox.size.x;
			if (Physics2D.LinecastNonAlloc(hitboxBottom, hitboxBottom - Vector2.up * 0.02f, result, LayerMask.GetMask("Obstacle")) > 0)
				ground = true;
		}
		anim.SetBool("Ground", ground);

		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
			bool direction = true;
			if (Input.GetKey(KeyCode.A))
				direction = false;
			float speed = (5 - crouching * 2.5f) * (direction ? 1 : -1) * (onLadder ? 0.5f : 1.0f);
			transform.localScale = new Vector3((direction ? 1 : -1) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			//transform.position += speed * Vector3.right;
			body.velocity = new Vector2(speed, body.velocity.y);
			anim.SetFloat("Movement", 1);
		} else {
			body.velocity = new Vector2(0, body.velocity.y);
			anim.SetFloat("Movement", 0);
		}

		if (Input.GetKeyDown(KeyCode.Space) && ground)
			body.velocity += new Vector2(0, 7 - crouching * 1);
		if (!ground && !Input.GetKey(KeyCode.Space) && body.velocity.y > 0)
			body.velocity = new Vector2(body.velocity.x, 0);

		if (Input.GetKeyDown(KeyCode.C))
			anim.SetFloat("Crouching", Mathf.Abs(crouching - 1));
		float height = 1.25f;
		float crouchHeight = 0.8f;
		float crouchSpeed = 2.5f;
		if (anim.GetFloat("Crouching") > 0) {
			hitbox.offset = Vector2.MoveTowards(hitbox.offset, new Vector2(0, crouchHeight / 2), Time.deltaTime * crouchSpeed);
			hitbox.size = Vector2.MoveTowards(hitbox.size, new Vector2(hitbox.size.x, crouchHeight), Time.deltaTime * crouchSpeed);
		} else {
			hitbox.offset = Vector2.MoveTowards(hitbox.offset, new Vector2(0, height / 2), Time.deltaTime * crouchSpeed);
			hitbox.size = Vector2.MoveTowards(hitbox.size, new Vector2(hitbox.size.x, height), Time.deltaTime * crouchSpeed);
		}

		if (onLadder)
			if (!Physics2D.Raycast(topPoint, -Vector2.up, 0.01f, LayerMask.GetMask("Ladder")))
				onLadder = false;
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) {
			if (Physics2D.Raycast(topPoint, -Vector2.up, 0.01f, LayerMask.GetMask("Ladder"))) {
				Vector2 prev = transform.position;
				float climbSpeed = 2 * Time.deltaTime;
				transform.position += climbSpeed * Vector3.up * ((Input.GetKey(KeyCode.W)) ? 1 : -1);
				anim.SetFloat("Crouching", 0.0f);
				onLadder = true;
				if (!Physics2D.Raycast(topPoint, -Vector2.up, 0.01f, LayerMask.GetMask("Ladder")))
					transform.position = prev;
            }
		}

		if (Input.GetKeyDown(KeyCode.F))
			Takedown();

		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			Vector2 mousePos = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 direction = mousePos -
				new Vector2(transform.position.x, transform.position.y);
			Rigidbody2D rock = ((GameObject)Instantiate(rockPrefab,
				midPoint, Quaternion.identity)).GetComponent<Rigidbody2D>();
			rock.AddForce(direction.normalized * 40);
		}
	}

	public void Takedown() {
		Vector2 origin = new Vector2((transform.localScale.x > 0 ? hitbox.bounds.max.x + 0.001f : hitbox.bounds.min.x - 0.001f), (hitbox.bounds.min.y + hitbox.bounds.max.y) / 2);
		float distance = 0.25f;
		Vector2 direction = transform.localScale.x > 0 ? Vector2.right : -Vector2.right;
		Debug.DrawLine(origin, origin + direction * distance, Color.red, 1.0f);
	}
}