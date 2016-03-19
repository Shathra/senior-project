using UnityEngine;
using System.Collections;

public class PlayerGhost : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnTriggerEnter2D(Collider2D col)
    {
        Guardian guard = col.gameObject.GetComponent<Guardian>();
        if (guard != null && transform.GetChild(0).gameObject.activeInHierarchy)
        {
            guard.state.SetState(Status.Searching);
            transform.GetChild(0).gameObject.SetActive(false);
            //todo
        }
    }
}
