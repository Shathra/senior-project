using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    public bool isLevelEnd;
    public Door otherDoor;

    private Player player;

    void OnTriggerEnter2D(Collider2D col)
    {
        player = col.gameObject.GetComponent<Player>();
        if (player != null)
        {
            if(player.carriesTarget)
            {
                GameController.GameWon();
            }
        }
    }
}
