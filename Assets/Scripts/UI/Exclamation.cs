using UnityEngine;
using System.Collections;

public enum ExclamationType {
    Suspicious, Alerted
}

public class Exclamation : MonoBehaviour {
    public ExclamationType type;

    void Start() {
        if (type == ExclamationType.Suspicious)
            GetComponent<SpriteRenderer>().color = Color.yellow;
        else if (type == ExclamationType.Alerted)
            GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void Over() {
        Destroy(gameObject);
    }
}
