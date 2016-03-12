using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum Status
{
    Idle = 1,       // Player has not been seen once
    HasSeen = 2,    // Player has been seen at least once but evaded enemies
    Suspicious = 3,    // Enemy is suspicious of a noise
    Searching = 4,  // Player has been seen but whereabouts are lost, enemies search whole local area
    Alert = 5     // Player is seen and all enemies are alert
}

/// <summary>
/// Represent state of an enemy (Idle, searching, hostile etc.)
/// </summary>
public class State {
    Status stt;
    public State() {
        stt = Status.Idle;
    }
    public Status GetState()
    {
        return stt;
    }
    public void SetState(Status s)
    {
        stt = s;
    }
}