using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for all type of enemies
/// </summary>
public class Enemy : MonoBehaviour {

    public ActionQueue actionQueue { get; set; }
    public State state;

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
    void Idle()
    {

    }
    void Searching()
    {

    }
    void Caution()
    {

    }
    void Cautious()
    {

    }
    void Hostile()
    {

    }
}