using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {
    private ParticleSystem particles;
    private CircleCollider2D col;
    private bool soundEmitted;
    private Guardian guard;

    void Start() {
        particles = GetComponent<ParticleSystem>();
        col = GetComponent<CircleCollider2D>();
        soundEmitted = false;
    }

    void Update() {
        if (particles.isStopped)
            Destroy(gameObject);
    }

    void FixedUpdate() {
        if (soundEmitted)
            col.radius = 0;
        else
            soundEmitted = true;
    }

    void OnTriggerEnter2D(Collider2D col) {
        guard = col.gameObject.GetComponent<Guardian>();
        if (guard != null) {
            if (guard.actionQueue.Peek() != null && guard.actionQueue.Peek().priority < 2)
            {
                guard.actionQueue.Insert(new SearchAction(transform.position));
            }
        }
    }

    public static void GenerateSound(Vector2 loc, float radius) {
        Vector3 pos = new Vector3(loc.x, loc.y, 0);
        ((GameObject)Instantiate(Player.instance.soundPrefab, pos, Quaternion.identity))
            .transform.localScale = new Vector3(radius, radius, radius);
    }
}
