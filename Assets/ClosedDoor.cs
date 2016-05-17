using UnityEngine;
using System.Collections;

public class ClosedDoor : MonoBehaviour {
    public Sprite open;
    public void Activate()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = open;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

    }
}
