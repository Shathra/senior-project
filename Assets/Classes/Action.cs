using UnityEngine;
using System.Collections;

public abstract class Action {
    public int priority { get; set; }

    public Action(int priority = 0) {
        this.priority = priority;
    }

    public virtual void Execute(Enemy enemy) {

    }
}
