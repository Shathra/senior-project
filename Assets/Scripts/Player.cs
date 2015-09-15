using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public GameObject rockPrefab;

    void Update() {
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * Time.deltaTime * 5);
        else if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * Time.deltaTime * 5);

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos -
                new Vector2(transform.position.x, transform.position.y);
            Rigidbody2D rock = ((GameObject)Instantiate(rockPrefab, 
                transform.position, Quaternion.identity)).GetComponent<Rigidbody2D>();
            rock.AddForce(direction.normalized * 40);
        }
    }
}
