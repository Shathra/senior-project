using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	void Start() {
		Sound.GenerateSound(transform.position, 10);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.layer == LayerMask.NameToLayer("Obstacle")) {
			Sound.GenerateSound(transform.position, 5);
			Destroy(gameObject);
		} else if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
			Destroy(gameObject);
			//Destroy(col.gameObject);
			GameController.GameLost();
		}
	}

}
