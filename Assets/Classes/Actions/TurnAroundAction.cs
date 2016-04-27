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
    float numberOfPeriods;
    float timeLeft;
    bool isInfinite;

    public TurnAroundAction(float period)
        : base(PRIORITY_IDLE) {
        this.period = period;
        timeLeft = period;
        isInfinite = true;
    }
    public TurnAroundAction(float period, float numberOfPeriods, int priority)
        : base(priority) {
        this.period = period;
        timeLeft = period;
        isInfinite = false;
        this.numberOfPeriods = numberOfPeriods;
    }
    
    public override void Execute(Enemy enemy)
    {
        //once every time   
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else if (((Guardian)enemy).angle < 30  )
        {
            ((Guardian)enemy).angle = Mathf.Lerp(((Guardian)enemy).angle, 40, Time.deltaTime);
            ((Guardian)enemy).TurnHead();
        } else {
            ((Guardian)enemy).angle = 0;
            ((Guardian)enemy).TurnHead();
            ((Guardian)enemy).direction = !((Guardian)enemy).direction;
            timeLeft = period;
            if (!isInfinite) {
                numberOfPeriods--;
                if (numberOfPeriods <= 0) {
                    done = true;
                    if (priority == PRIORITY_SEARCH) {
                        PlayerGhost.instance.SetActive(false);
                    }
                    return;
                }
            }
        }
    }
}