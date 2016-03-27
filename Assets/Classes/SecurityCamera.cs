using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Immobile unit which alerts other enemy in a range when it sees a player
/// </summary>
public class SecurityCamera : Enemy, ISpotable {
    private const float ROTATION_AMOUNT = 85;
    public const float RANGE = 10;

    public float visionAngle { get; set; }
	public Vision vision { get; set; }

	private MeshFilter visionMesh;
    private PolygonCollider2D visionCollider;

    private bool direction;

    void Start() {
        direction = false;
        visionAngle = 30;
        vision = GetComponentInChildren<Vision>();
		vision.spotable = this;
        visionMesh = vision.gameObject.GetComponent<MeshFilter>();
        visionCollider = vision.gameObject.GetComponent<PolygonCollider2D>();
        vision.GenerateVision(visionAngle, RANGE);
    }

    void FixedUpdate() {
        transform.eulerAngles = new Vector3(0, 0,
            Mathf.MoveTowards(transform.eulerAngles.z, direction ? 180 - ROTATION_AMOUNT : 180 + ROTATION_AMOUNT, Time.fixedDeltaTime * 40));
        if (Mathf.Abs(transform.eulerAngles.z - (180 - ROTATION_AMOUNT)) < 1)
            direction = false;
        else if (Mathf.Abs(transform.eulerAngles.z - (180 + ROTATION_AMOUNT)) < 1)
            direction = true;
    }

    private Guardian NearestGuard() {
        List<Guardian> guards = AIController.GetGuardians();
        Guardian guard = guards[0];
        float smallestDistance = Mathf.Abs(Vector2.Distance(guard.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position));
        for (int i = 1; i < guards.Count; i++)
        {
            float distance = Mathf.Abs(Vector2.Distance(guards[0].transform.position, GameObject.FindGameObjectWithTag("Player").transform.position));
            if ( distance < smallestDistance)
            {
                smallestDistance = distance;
                guard = guards[0];
            }
        }
        return guard;
    }

	public void Spot(GameObject obj) {
		Guardian nearestGuard = NearestGuard();
		nearestGuard.Spot(Player.instance.gameObject);
	}

	public void SpotOut(GameObject obj) {
		Spot(obj);
	}
}
