using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
    public Player player;

    void Update() {
        Vector2 pos = player.midPoint;
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }
}
