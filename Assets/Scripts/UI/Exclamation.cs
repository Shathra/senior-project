using UnityEngine;
using System.Collections;

public enum ExclamationType {
    Suspicious, Alerted
}

public class Exclamation : MonoBehaviour {
    public ExclamationType type;

    private SpriteRenderer sprite;

    void Start() {
        sprite = GetComponent<SpriteRenderer>();
        if (type == ExclamationType.Suspicious)
            sprite.color = Color.yellow;
        else if (type == ExclamationType.Alerted)
            sprite.color = Color.red;
    }

    void Update() {
        if (sprite.color.a <= 0) {
            Destroy(gameObject);
            return;
        }
        transform.position = new Vector2(transform.position.x,
            transform.position.y + Time.deltaTime / 2);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b,
            sprite.color.a - Time.deltaTime);
    }
}
