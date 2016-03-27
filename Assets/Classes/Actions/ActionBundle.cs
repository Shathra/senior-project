using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// ActionBundle specifies a sequence of actions which will be executed in order (in queue manner)
/// All actions in the ActionBundle share a common priority
/// </summary>
class ActionBundle: Action {
    protected List<Action> actions;

    public ActionBundle() : base(Action.PRIORITY_SEARCH){
    }

    /// <summary>
    /// Add action to queue.
    /// </summary>
    /// <param name="action">Action to be added</param>
    public void Add( Action action) {

        actions.Add(action);
    }

    public override void Execute(Enemy enemy) {

        if( actions.Count == 0) {

            done = true;
        }

        else {

            if( actions[0].IsDone()) {

                actions.Remove(actions[0]);
                Execute(enemy);
            }

            else {

                actions[0].Execute(enemy);
            }
        }
    }
}