using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for all type of enemies
/// </summary>
public class Enemy : MonoBehaviour {

    public ActionQueue actionQueue { get; set; }

    public void Start()
    {
        actionQueue = new ActionQueue();
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