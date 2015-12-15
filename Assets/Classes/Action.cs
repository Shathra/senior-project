using UnityEngine;
using System.Collections;

public abstract class Action {
    public int priority { get; set; }
	public bool done { get; set; }

    public Action(int priority = 0) {
        this.priority = priority;
		done = false;
    }

    public virtual void Execute(Enemy enemy) {

    }
}
