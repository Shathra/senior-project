using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NightVision : MonoBehaviour {
	private const float REGEN = 0.25f;

	public NightVisionSkill skill { get; set; }
	private bool _running;
	public bool running {
		get {
			return _running;
		}
		set {
			_running = value;
			Player.instance.darkness.material.SetColor("_TintColor", running ? new Color(0, 1, 0, 0.3f) : Color.black);
		}
	}

	private float maxDuration;
	private float duration;
	private Image image;
	private Image fill;

	void Awake() {
		image = GetComponent<Image>();
		fill = transform.GetChild(0).GetComponent<Image>();
	}

	void Start() {
		running = false;
		maxDuration = skill.charges / 1000.0f;
		duration = maxDuration;
		transform.parent = UIController.canvas.transform;
		GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
	}

	void Update() {
		fill.fillAmount = duration / maxDuration;
		skill.charges = (int)(duration * 1000);
		if (running) {
			if (duration <= 0) {
				skill.charges = 0;
				running = false;
				return;
			}
			duration -= Time.deltaTime;
		} else {
			duration = Mathf.Min(duration + (REGEN * Time.deltaTime), maxDuration);
		}
	}
}
