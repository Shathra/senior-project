using UnityEngine;
using System.Collections;

public class FakeTarget : MonoBehaviour {

    public Sprite sprite1;
    public Sprite sprite2;

    // Use this for initialization
    void Start () {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite1;
    }
	
	// Update is called once per frame
	void Update () {

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite2;
        }
    }
}
