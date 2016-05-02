using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour {
	public void BackToMenu() {
		SceneManager.LoadScene("MainMenu");
	}

	public void Retry() {
		GameController.GameOver();
	}
}
