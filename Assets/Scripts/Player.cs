using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public GameObject rockPrefab;

    private Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }

    void Update() {
        bool crouching = anim.GetBool("Crouching");

        if (Input.GetKey(KeyCode.A)) {
            transform.position += Vector3.left * Time.deltaTime * 5;
        } else if (Input.GetKey(KeyCode.D)) {
            transform.position += Vector3.right * Time.deltaTime * 5;
        }

        if (Input.GetKeyDown(KeyCode.C))
            anim.SetBool("Crouching", !crouching);

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
