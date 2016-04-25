using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {
    private ParticleSystem particles;
    private CircleCollider2D col;
    private bool soundEmitted;

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
        Guardian guard = col.gameObject.GetComponent<Guardian>();
        if (guard != null) {
            //guard.actionQueue.Insert(new ApproachAction(guard.transform.position, transform.position, Action.PRIORITY_SEARCH_APPROACH));
        }
    }

    public static void GenerateSound(Vector2 loc, float radius) {
        ((GameObject)Instantiate(Player.instance.soundPrefab, (Vector3)loc, Quaternion.identity))
            .transform.localScale = new Vector3(radius, radius, radius);
    }
}
