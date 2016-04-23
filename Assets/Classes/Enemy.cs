using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for all type of enemies
/// </summary>
public class Enemy : MonoBehaviour {

    public ActionQueue actionQueue { get; set; }
    public State state;

    private bool _direction;
    public bool direction
    {
        get
        {
            return _direction;
        }
        set
        {
            _direction = value;
            transform.GetChild(0).transform.localScale = new Vector3((_direction ? -1 : 1) * Mathf.Abs(transform.GetChild(0).transform.localScale.x), transform.GetChild(0).transform.localScale.y, transform.GetChild(0).transform.localScale.z);
            transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.localScale = new Vector3((_direction ? -1 : 1) * Mathf.Abs(transform.GetChild(0).transform.localScale.x), transform.GetChild(0).transform.localScale.y, transform.GetChild(0).transform.localScale.z);
            transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.Rotate(new Vector3(0f, 0f, 180f));
        }
    }

    public void Awake()
    {
        actionQueue = new ActionQueue();
        state = new State();
    }

    public virtual void Update()
    {
        Action action = actionQueue.Peek();
        if (action != null)
        {
            action.Execute(this);
            if (action.done)
                actionQueue.Remove();
        }
    }

}