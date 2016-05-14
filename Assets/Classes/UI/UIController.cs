using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {
	public static UIController canvas { get; private set; }

	public GameObject skillWheel;
	public GameObject gameOver;
	public Image dim;
	public GameObject pauseMenu;

    public bool pauseMenuActive;

	void Awake() {
		canvas = this;
	}

	void Start() {
		skillWheel.SetActive(false);
        pauseMenuActive = false;
	}

	void Update() {
		if ((Input.GetKeyDown(KeyCode.Tab) || (XInputTestCS.prevState.Triggers.Left < 0.1 && XInputTestCS.state.Triggers.Left > 0.1)) && !pauseMenu.activeInHierarchy) {
			skillWheel.SetActive(true);
			Player.instance.playerLock = true;
			Time.timeScale = 0;
		} else if ((Input.GetKeyDown(KeyCode.Escape) || (XInputTestCS.prevState.Buttons.Start != 0 && XInputTestCS.state.Buttons.Start == 0)) && !skillWheel.activeInHierarchy) {
			pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
			Time.timeScale = pauseMenu.activeInHierarchy ? 0 : 1;
            pauseMenuActive = !pauseMenuActive;
		}

        if (pauseMenuActive)
        {
            if (XInputTestCS.prevState.Buttons.A != XInputTestCS.state.Buttons.A)
            {
                pauseMenu.GetComponent<GameOver>().Retry();
            }
            else if (XInputTestCS.prevState.Buttons.Y != XInputTestCS.state.Buttons.Y)
            {
                pauseMenu.GetComponent<GameOver>().BackToMenu();
            }
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
