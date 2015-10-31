using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public GameObject rockPrefab;

    private Animator anim;
    private BoxCollider2D hitbox;
    private Rigidbody2D body;

    void Start() {
        anim = GetComponent<Animator>();
        hitbox = GetComponentInChildren<BoxCollider2D>();
        body = GetComponent<Rigidbody2D>();
    }

    public Vector2 topPoint {
        get {
            return new Vector2(hitbox.bounds.max.x - hitbox.size.x / 2, hitbox.bounds.max.y);
        }
    }

    public Vector2 midPoint {
        get {
            return new Vector2(hitbox.bounds.min.x + hitbox.size.x / 2, hitbox.bounds.min.y + hitbox.size.y / 2);
        }
    }

    public Vector2 bottomPoint {
        get {
            return new Vector2(hitbox.bounds.min.x + hitbox.size.x / 2, hitbox.bounds.min.y);
        }
    }

    void Update() {
        float crouching = anim.GetFloat("Crouching");
        bool ground = false;
        Vector2 hitboxBottom = new Vector2(hitbox.bounds.min.x, hitbox.bounds.min.y);
        RaycastHit2D[] result = new RaycastHit2D[1];
        if (Physics2D.LinecastNonAlloc(hitboxBottom, hitboxBottom - Vector2.up * 0.02f, result, LayerMask.GetMask("Obstacle")) > 0)
            ground = true;
        else {
            hitboxBottom += Vector2.right * hitbox.size.x;
            if (Physics2D.LinecastNonAlloc(hitboxBottom, hitboxBottom - Vector2.up * 0.02f, result, LayerMask.GetMask("Obstacle")) > 0)
                ground = true;
        }
        anim.SetBool("Ground", ground);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
            float angle = 90;
            float speed = Time.deltaTime * (5 - crouching * 2.5f);
            if (Input.GetKey(KeyCode.A)) {
                angle = 270;
                speed = -speed;
            }
            transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, angle, transform.rotation.eulerAngles.z);
            transform.position += speed * Vector3.right;
            anim.SetFloat("Movement", 1);
        } else {
            anim.SetFloat("Movement", 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && ground)
            body.velocity += new Vector2(0, 7 - crouching * 1);
        if (!ground && !Input.GetKey(KeyCode.Space) && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x, 0);

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
