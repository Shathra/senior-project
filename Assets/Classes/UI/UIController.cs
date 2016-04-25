using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {
	public GameObject skillWheel;

	void Start() {
		skillWheel.SetActive(false);
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Tab)) {
			skillWheel.SetActive(true);
			Player.instance.playerLock = true;
			Time.timeScale = 0;
		}
		if (Input.GetKeyUp(KeyCode.Tab)) {
			skillWheel.SetActive(false);
			Player.instance.playerLock = false;
			Time.timeScale = 1;
		}
	}
}
