
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AIController {

    protected static AIManager manager;

    public static Vector2 lastKnownPosition;

    public static void Init( AIManager initManager) {

        manager = initManager;
    }

    public static Node GetNearestNode(Vector2 pos) {

        return manager.GetNearestNode(pos);
    }

    public static List<Node> GetPath( Vector2 source, Vector2 target) {

        return manager.GetPath(source, target);
    }

    public static Player GetPlayer() {

        return manager.GetPlayer();
    }

    public static List<Enemy> GetEnemies() {

        return manager.GetEnemies();
    }
}
