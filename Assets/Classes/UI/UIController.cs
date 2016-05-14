using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {
	public static UIController canvas { get; private set; }

	public GameObject skillWheel;
	public GameObject gameOver;
	public Image dim;
	public GameObject pauseMenu;

	void Awake() {
		canvas = this;
	}

	void Start() {
		skillWheel.SetActive(false);
	}

	void Update() {
		if ((Input.GetKeyDown(KeyCode.Tab) || (XInputTestCS.prevState.Triggers.Left < 0.1 && XInputTestCS.state.Triggers.Left > 0.1)) && !pauseMenu.activeInHierarchy) {
			skillWheel.SetActive(true);
			Player.instance.playerLock = true;
			Time.timeScale = 0;
		} else if ((Input.GetKeyDown(KeyCode.Escape) || (XInputTestCS.prevState.Buttons.Start != 0 && XInputTestCS.state.Buttons.Start == 0)) && !skillWheel.activeInHierarchy) {
			pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
			Time.timeScale = pauseMenu.activeInHierarchy ? 0 : 1;
		}
	}
	void LateUpdate() {
		if ((Input.GetKeyUp(KeyCode.Tab) || (XInputTestCS.prevState.Triggers.Left > 0.1 && XInputTestCS.state.Triggers.Left < 0.1)) && !pauseMenu.activeInHierarchy) {
			skillWheel.SetActive(false);
			Player.instance.playerLock = false;
			Time.timeScale = 1;
		}
	}
}
