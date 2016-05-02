using UnityEngine;
using System.Collections;

public class SpiderCam : MonoBehaviour
{
    private const float RANGE = 20;
    private Vector2 startingPoint;
    private Rigidbody2D body;
    bool snapped;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        startingPoint = transform.position;
        snapped = false;
    }

    void Update()
    {
        if (snapped)
        {
            transform.localPosition = Vector3.zero;
        }
        if (Vector2.Distance(transform.position, startingPoint) > RANGE)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Obstacle") || col.gameObject.layer == LayerMask.NameToLayer("Enemy") || col.gameObject.layer == LayerMask.NameToLayer("Guardian"))
        {
            body.velocity = Vector3.zero;
            body.angularVelocity = 0f;

        }
        if (col.gameObject.layer == LayerMask.NameToLayer("Guardian"))
        {
            snapped = true;
            transform.SetParent(col.gameObject.transform);
        }
    }
}
