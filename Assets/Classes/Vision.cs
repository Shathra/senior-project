using UnityEngine;
using System.Collections;

public class Vision : MonoBehaviour {
    private Enemy enemy;
    
	void Start () {
        enemy = GetComponentInParent<Enemy>();
	}

    void OnTriggerEnter2D(Collider2D col) {
        ((ISpotable) enemy).Spot(col.gameObject);
    }
}
