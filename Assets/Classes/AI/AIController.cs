using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AIController {

    protected static AIManager manager;

    public static void Init( AIManager initManager) {

        manager = initManager;
    }

    public static List<Node> GetPath( Vector2 source, Vector2 target) {

        return manager.GetPath(source, target);
    }
}
