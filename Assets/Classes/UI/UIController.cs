using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {
	public static UIController canvas { get; private set; }

	public GameObject skillWheel;
	public GameObject gameOver;
	public Image dim;

	void Awake() {
		canvas = this;
	}

	void Start() {
		skillWheel.SetActive(false);
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Tab) || (XInputTestCS.prevState.Triggers.Left < 0.1 &&  XInputTestCS.state.Triggers.Left > 0.1)) {
			skillWheel.SetActive(true);
			Player.instance.playerLock = true;
			Time.timeScale = 0;
		}
	}
    void LateUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Tab) || (XInputTestCS.prevState.Triggers.Left > 0.1 && XInputTestCS.state.Triggers.Left < 0.1))
        {
            skillWheel.SetActive(false);
            Player.instance.playerLock = false;
            Time.timeScale = 1;
        }
    }
}
