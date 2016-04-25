using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Immobile unit which alerts other enemy in a range when it sees a player
/// </summary>
public class SecurityCamera : MonoBehaviour, ISpotable {
    private const float ROTATION_AMOUNT = 85;
    public const float RANGE = 10;

    public float visionAngle { get; set; }
	public Vision vision { get; set; }
	public float rotationSpeed { get; set; }

	private MeshFilter visionMesh;

    private bool direction;

    //From functions
    List<Guardian> guards;
    Guardian guard;
    Guardian nearestGuard;

    void Start() {
        direction = false;
        visionAngle = 30;
        vision = GetComponentInChildren<Vision>();
		vision.spotable = this;
        visionMesh = vision.gameObject.GetComponent<MeshFilter>();
		rotationSpeed = MLLevelStats.GetStat(LevelStat.CameraAngularSpeed);
		vision.gameObject.GetComponent<FOV2DEyes>().fovMaxDistance = MLLevelStats.GetStat(LevelStat.CameraAwarenessRange);
		//vision.GenerateVision(visionAngle, RANGE);
	}

    void FixedUpdate() {
        transform.eulerAngles = new Vector3(0, 0,
            Mathf.MoveTowards(transform.eulerAngles.z, direction ? 180 - ROTATION_AMOUNT : 180 + ROTATION_AMOUNT, Time.fixedDeltaTime * rotationSpeed));
        if (Mathf.Abs(transform.eulerAngles.z - (180 - ROTATION_AMOUNT)) < 1)
            direction = false;
        else if (Mathf.Abs(transform.eulerAngles.z - (180 + ROTATION_AMOUNT)) < 1)
            direction = true;
    }

    private Guardian NearestGuard() {
        guards = AIController.GetGuardians();
        guard = guards[0];
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

	public void Spot() {
		nearestGuard = NearestGuard();
        if (nearestGuard.actionQueue.Peek().GetType() != typeof(FireAction)) {
            while (nearestGuard.actionQueue.Peek().priority == Action.PRIORITY_SEARCH || nearestGuard.actionQueue.Peek().priority == Action.PRIORITY_SEARCH_APPROACH){
                    nearestGuard.actionQueue.Remove();
            }
            nearestGuard.actionQueue.Insert(new SearchAction(Player.instance));
        }
	}

	public void SpotOut() {
		
	}
}
