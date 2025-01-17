﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public GameObject levelSelect;

	private const int FRAME_AMOUNT = 47;

	private Sprite[] frames;
	private Image back;
	private float time;
	
	void Start () {
		frames = new Sprite[FRAME_AMOUNT];
		for(int i = 1; i <= FRAME_AMOUNT; i++) {
			frames[i - 1] = Resources.Load<Sprite>("UI/MainMenuFrames/startui_" + i.ToString("D5"));
		}
		back = GetComponent<Image>();
		time = 0;
	}
	
	void Update () {
		while (time > 4)
			time -= 4;
		back.sprite = frames[(int)(time * 24) % FRAME_AMOUNT];
		time += Time.deltaTime;
	}

	public void Exit() {
		Application.Quit();
	}

	public void ShowLevelSelect() {
		levelSelect.SetActive(true);
	}

	public void HideLevelSelect() {
		levelSelect.SetActive(false);
	}

	public void LoadLevel(string name) {
		SceneManager.LoadScene(name);
	}
}
