using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D col) {
		Guardian guard = col.gameObject.GetComponent<Guardian>();
        if (guard != null) {
			
		}
	}

	public static void GenerateSound(Vector2 loc, float radius) {
		((GameObject)Instantiate(Player.instance.soundPrefab, (Vector3)loc, Quaternion.identity))
			.transform.localScale = new Vector3(radius, radius, radius);
	}
}
