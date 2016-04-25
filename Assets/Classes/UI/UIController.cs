using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {
	public GameObject skillWheel;

	void Start() {
		skillWheel.SetActive(false);
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.LeftControl)) {
			skillWheel.SetActive(true);
			Player.instance.playerLock = true;
			Time.timeScale = 0;
		}
		if (Input.GetKeyUp(KeyCode.LeftControl)) {
			skillWheel.SetActive(false);
			Player.instance.playerLock = false;
			Time.timeScale = 1;
		}
	}
}
