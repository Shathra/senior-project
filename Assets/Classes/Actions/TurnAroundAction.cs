using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Specifies action which source patrol between two points
/// </summary>
class TurnAroundAction : Action
{
    float period;
    float timeLeft;
    public TurnAroundAction(float period)
    {
        this.period = period;
        timeLeft = period;
    }
    
    public override void Execute(Enemy enemy)
    {
        //once every time     
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }else{
            ((Guardian)enemy).direction = !((Guardian)enemy).direction;
            timeLeft = period;
        }
    }
}