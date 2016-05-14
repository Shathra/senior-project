using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour {
	public void BackToMenu() {
		SceneManager.LoadScene("MainMenu");
		Time.timeScale = 1;
	}

	public void Retry() {
		GameController.GameOver();
		Time.timeScale = 1;
	}
}
