using UnityEngine;
using System.Collections;

public abstract class Action {

    public static readonly int PRIORITY_DEFAULT = 0;
    public static readonly int PRIORITY_SEARCH = 1;
    public static readonly int PRIORITY_HOSTILE = 2;

    public int priority { get; set; }
	public bool done { get; set; }

    public Action(int priority = 0) {
        this.priority = priority;
		done = false;
    }

    public virtual void Execute(Enemy enemy) {

    }

    public bool IsDone() {

        return done;
    }
}
