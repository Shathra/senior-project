using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Trailer : MonoBehaviour {
	private const float INACTIVE_DURATION = 30;

	public GameObject mainMenu;
	public RawImage video;

	private Vector2 prevMousePos;
	private float timer;
	
	void Start () {
		video.gameObject.SetActive(false);
		prevMousePos = Vector2.zero;
		timer = INACTIVE_DURATION;
	}
	
	void Update () {
		if (!mainMenu.activeInHierarchy && !video.gameObject.activeInHierarchy)
			return;

		if(timer > 0)
			timer -= Time.deltaTime;

		Vector2 mousePos = Input.mousePosition;
		if (Vector2.Distance(mousePos, prevMousePos) > 0.1f)
			timer = INACTIVE_DURATION;
        prevMousePos = mousePos;

		if (Input.anyKeyDown)
			timer = INACTIVE_DURATION;

		if (timer <= 0)
			Play();
		else if(video.gameObject.activeInHierarchy)
			Stop();
	}

	public void Play() {
		video.gameObject.SetActive(true);
		((MovieTexture)video.texture).loop = true;
		((MovieTexture)video.texture).Play();
		mainMenu.SetActive(false);
	}

	public void Stop() {
		mainMenu.SetActive(true);
		video.gameObject.SetActive(false);
		((MovieTexture)video.texture).Stop();
	}
}
