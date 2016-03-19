using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.layer == LayerMask.NameToLayer("Obstacle")) {
			Destroy(gameObject);
		} else if(col.gameObject.layer == LayerMask.NameToLayer("Player")) {
			Destroy(gameObject);
            //Destroy(col.gameObject);
            GameController.GameLost();
		}
	}

}
