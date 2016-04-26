using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroController : MonoBehaviour {
	public Image fader;
	public RawImage intro;
	public GameObject menu;

	private MovieTexture movie;

	void Start() {
		intro.gameObject.SetActive(true);
		menu.gameObject.SetActive(false);
		movie = (MovieTexture)intro.texture;
		movie.Play();
	}

	void Update() {
		if (intro.gameObject.activeSelf) {
			if (!movie.isPlaying) {
				fader.color = Color.LerpUnclamped(fader.color, Color.black, Time.deltaTime * 3);
				if (fader.color.a >= 0.99f) {
					intro.gameObject.SetActive(false);
					menu.gameObject.SetActive(true);
				}
			}
		} else if (menu.gameObject.activeSelf) {
			if (fader.gameObject.activeSelf) {
				fader.color = Color.LerpUnclamped(fader.color, Color.clear, Time.deltaTime * 3);
				if (fader.color.a <= 0.01f)
					fader.gameObject.SetActive(false);
			}
		}

		if (Input.GetKeyDown(KeyCode.Return))
			movie.Stop();
	}
}
