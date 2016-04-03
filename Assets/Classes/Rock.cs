using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.layer == LayerMask.NameToLayer("Obstacle")) {
			Destroy(gameObject);
		}
    }

	void OnDestroy() {
		Sound.GenerateSound(transform.position, 2);
	}
}
