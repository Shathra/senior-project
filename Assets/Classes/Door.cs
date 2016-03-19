using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    public bool isLevelEnd;
    public Door otherDoor;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.gameObject.GetComponent<Player>();
        if (player != null)
        {
            if(player.carriesTarget)
            {
                GameController.GameWon();
            }
        }
    }
}
