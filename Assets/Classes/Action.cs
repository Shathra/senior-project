using UnityEngine;
using System.Collections;

public abstract class Action {

    public static readonly int PRIORITY_IDLE = 0;
    public static readonly int PRIORITY_IDLE_APPROACH = 1;
    public static readonly int PRIORITY_SEARCH = 2;
    public static readonly int PRIORITY_SEARCH_APPROACH = 3;
    public static readonly int PRIORITY_HOSTILE = 4;

    public int priority { get; set; }
	public bool done { get; set; }

    public Action() {
        this.priority = 0;
		done = false;
    }
    public Action(int priority) {
        this.priority = priority;
        done = false;
    }

    public virtual void Execute(Enemy enemy) {

    }

    public bool IsDone() {

        return done;
    }
}
