using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public GameObject blastPrefab;

    private Rigidbody2D body;

	void Start() {
        body = GetComponent<Rigidbody2D>();
		Sound.GenerateSound(transform.position, 10);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.layer == LayerMask.NameToLayer("Obstacle")) {
			Sound.GenerateSound(transform.position, 5);
            Instantiate(blastPrefab, transform.position, Quaternion.FromToRotation(Vector3.down, Vector3.Normalize(body.velocity)));
			Destroy(gameObject);
		} else if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
            Instantiate(blastPrefab, transform.position, Quaternion.FromToRotation(Vector3.down, Vector3.Normalize(body.velocity)));
            Destroy(gameObject);
			//Destroy(col.gameObject);
            //GameController.GameLost();
		}
	}

}
