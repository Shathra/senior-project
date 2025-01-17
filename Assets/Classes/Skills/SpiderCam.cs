﻿using UnityEngine;
using System.Collections;

public class SpiderCam : MonoBehaviour
{
    private const float RANGE = 20;
    private Vector2 startingPoint;
    private Rigidbody2D body;
    private Animator anim;
    bool snapped;

    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        startingPoint = transform.position;
        snapped = false;

        body.angularVelocity = 0f;
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
            anim.SetTrigger("Stick");
            body.angularVelocity = 0f;
            body.velocity = Vector3.zero;
            if (col.gameObject.layer == LayerMask.NameToLayer("Guardian"))
            {
                snapped = true;
                transform.SetParent(col.gameObject.transform);
            }

        }
    }
}
