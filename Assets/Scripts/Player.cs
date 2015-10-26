using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public GameObject rockPrefab;

    private Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }

    void Update() {
        float crouching = anim.GetFloat("Crouching");

        float speed = 5 - crouching * 2.5f;
        if (Input.GetKey(KeyCode.A)) {
            transform.position += Vector3.left * Time.deltaTime * speed;
            transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, 270, transform.rotation.eulerAngles.z);
            anim.SetFloat("Movement", 1);
        } else if (Input.GetKey(KeyCode.D)) {
            transform.position += Vector3.right * Time.deltaTime * speed;
            transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, 90, transform.rotation.eulerAngles.z);
            anim.SetFloat("Movement", 1);
        } else
            anim.SetFloat("Movement", 0);

        if (Input.GetKeyDown(KeyCode.C))
            anim.SetFloat("Crouching", Mathf.Abs(crouching - 1));

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
