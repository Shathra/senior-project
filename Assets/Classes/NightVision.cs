using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NightVision : MonoBehaviour {
	private const float MAX_DURATION = 5;

	private float duration;
	private Image image;

	void Start() {
		duration = MAX_DURATION;
		image = GetComponent<Image>();
	}

	void Update() {
		Color color = Player.instance.darkness.material.GetColor("_TintColor");
        image.color = new Color(image.color.r, image.color.g, image.color.b,
			duration / MAX_DURATION);
		if(duration <= 0) {
			Player.instance.darkness.material.SetColor("_TintColor", new Color(color.r, color.g, color.b, 1f));
			Destroy(gameObject);
			return;
		}
		duration -= Time.deltaTime;
		Player.instance.darkness.material.SetColor("_TintColor", new Color(color.r, color.g, color.b, 0.3f));
	}
}
