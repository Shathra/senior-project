using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for all type of enemies
/// </summary>
public class Enemy : MonoBehaviour {

    public ReactionAI reactionAI { get; set; }
    public ActionQueue actionQueue { get; set; }

    void Start()
    {
        reactionAI = new ReactionAI(this);
        actionQueue = new ActionQueue();
    }

    void Update()
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