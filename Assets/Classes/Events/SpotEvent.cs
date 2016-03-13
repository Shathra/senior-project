using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SpotEvent : GameEvent {

    public Vector2 Location { get; set; }

    public SpotEvent( Enemy source, Vector2 location) : base( source, GameEventType.EnemySpotted) {
    
        this.Location = location;
        
    }
}

