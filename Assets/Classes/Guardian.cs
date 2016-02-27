using UnityEngine;
using System.Collections;

public class Guardian : Enemy, ISpotable, IApproachable {

    protected float moveSpeed;
    protected float turnRate;

    public GameObject exclamationPrefab;

    public new void Start() {
 
        base.Start();
        this.moveSpeed = MLLevelStats.GuardianSpeed;
    }

    public void Approach(Vector2 target) {

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.x, transform.position.y), Time.deltaTime * moveSpeed);
    }

    public void Spot(GameObject obj) {
        Player player = obj.GetComponentInParent<Player>();
        if (player != null) {
            Exclamation ex = ((GameObject)Instantiate(exclamationPrefab,
                new Vector2(transform.position.x, transform.position.y + 1),
                Quaternion.identity)).GetComponent<Exclamation>();
            ex.transform.SetParent(gameObject.transform);
            ex.type = ExclamationType.Alerted;
            actionQueue.Insert(EventManager.Spot(new SpotEvent(this, player.midPoint)));
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
