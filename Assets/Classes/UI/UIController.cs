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
		if (Input.GetKeyDown(KeyCode.Tab)) {
			skillWheel.SetActive(true);
			Player.instance.playerLock = true;
			Time.timeScale = 0;
		}
	}
    void LateUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            skillWheel.SetActive(false);
            Player.instance.playerLock = false;
            Time.timeScale = 1;
        }
    }
}
