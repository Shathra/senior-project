using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public GameObject exclamationPrefab;

    void Start() {

    }

    void Update() {

    }

    public void Spot(GameObject obj) {
        Player player = obj.GetComponent<Player>();
        if (player != null) {
            Exclamation ex = ((GameObject)Instantiate(exclamationPrefab,
                new Vector2(transform.position.x, transform.position.y + 1),
                Quaternion.identity)).GetComponent<Exclamation>();
            ex.transform.SetParent(gameObject.transform);
            ex.type = ExclamationType.Alerted;
            return;
        }
        Rock rock = obj.GetComponent<Rock>();
        if (rock != null) {
            if (rock.state != RockState.Ended)
                return;
            Exclamation ex = ((GameObject)Instantiate(exclamationPrefab,
                new Vector2(transform.position.x, transform.position.y),
                Quaternion.identity)).GetComponent<Exclamation>();
            ex.transform.SetParent(gameObject.transform);
            ex.type = ExclamationType.Suspicious;
            return;
        }
    }
}
