using UnityEngine;
using System.Collections;

public class PlayerGhost : MonoBehaviour {
	public static PlayerGhost instance { get; private set; }
    public bool isActive;
    Guardian guard;
    // Use this for initialization
    void Start () {
		PlayerGhost.instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnTriggerEnter2D(Collider2D col)
    {
        guard = col.gameObject.GetComponent<Guardian>();
        if (guard != null && transform.GetChild(0).gameObject.activeInHierarchy)
        {
            guard.state.SetState(Status.Searching);
            transform.GetChild(0).gameObject.SetActive(false);
            isActive = false;
            //todo
        }
    }
    public void SetActive(bool isActive){
        transform.position = Player.instance.midPoint;
        transform.GetChild(0).gameObject.SetActive(isActive);
        this.isActive = isActive;
    }
}
