using UnityEngine;
using System.Collections;

public class SecurityCameraVision : MonoBehaviour {
	public SecurityCamera cam {	get; set; }

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
			Player player = col.gameObject.GetComponent<Player>();
            if (Physics2D.Raycast(transform.position, player.midPoint - (Vector2)transform.position, SecurityCamera.RANGE, LayerMask.GetMask("Obstacle", "Player")).collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
				cam.Spot(player);
			}
		}
	}
}
