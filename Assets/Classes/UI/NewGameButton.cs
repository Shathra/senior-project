using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NewGameButton : MonoBehaviour {
	public void LoadDemoLevel() {
		SceneManager.LoadScene("Demo_Scene");
	}
}
