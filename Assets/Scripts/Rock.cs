using UnityEngine;
using System.Collections;

public enum RockState {
    Flying, Collided, Ended
}

public class Rock : MonoBehaviour {
    public RockState state;

    private CircleCollider2D size;

    void Start() {
        state = RockState.Flying;
        size = GetComponent<CircleCollider2D>();
    }

    void FixedUpdate() {
        if (state == RockState.Collided)
            state = RockState.Ended;
        else if (state  == RockState.Ended)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.isTrigger)
            return;
        if (col.GetComponentInParent<Player>() != null)
            return;
        if (state == RockState.Ended) {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            if (enemy != null)
                enemy.Spot(gameObject);
        } else if (state == RockState.Flying) {
            state = RockState.Collided;
            size.radius = 1;
        }
    }
}
