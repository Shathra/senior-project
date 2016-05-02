using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {
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
	
	public void LoadDemoLevel() {
		SceneManager.LoadScene("Demo_Scene");
	}

	public void LoadTutorial() {

	}

	public void Exit() {
		Application.Quit();
	}
}
