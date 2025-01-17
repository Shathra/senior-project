﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Target : MonoBehaviour {
	public float requiredTime { get; set; }

	public Sprite sprite1;
	public Sprite sprite2;

    public ClosedDoor closedDoor;
    public bool activatesClosedDoor;

	private Text timerText;
	private Image timerImage;
	private float timer;
	private bool playerIn;
	private bool _taken;
	private bool taken {
		get {
			return _taken;
		}
		set {
			if (_taken == false && value == true) {
				Player.instance.carriesTarget = true;
				gameObject.GetComponent<SpriteRenderer>().sprite = sprite2;
                if (activatesClosedDoor)
                {
                    closedDoor.Activate();
                }
			}
			_taken = value;
		}
	}

	// Use this for initialization
	void Start() {
		gameObject.GetComponent<SpriteRenderer>().sprite = sprite1;
		requiredTime = MLLevelStats.GetStat(LevelStat.HackingTime);
		playerIn = false;
		timer = requiredTime;
		taken = false;
		timerText = GetComponentInChildren<Text>();
		timerImage = GetComponentInChildren<Image>();
	}

	// Update is called once per frame
	void Update() {
		if (playerIn)
			timer -= Time.deltaTime;
		if (timer <= 0)
			taken = true;
		timerText.text = (playerIn && !taken) ? "" + Mathf.Round(timer) : "";
		timerImage.fillAmount = (playerIn && !taken) ? timer / requiredTime : 0.0f;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			playerIn = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			playerIn = false;
			timer = requiredTime;
		}
	}
}
