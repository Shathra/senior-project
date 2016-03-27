using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmptyAction: Action {

    public EmptyAction() : base(Action.PRIORITY_HOSTILE) {

        done = true;
    }

    public new void Execute(Enemy enemy) {

    }
}
