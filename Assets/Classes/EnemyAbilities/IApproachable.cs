using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// It defines approach ability. That's classes implementing this interface can approach. (TODO: Is name bad? cuz it actually defines units which can approach not ones that other mobile units can approach to)
/// </summary>
public interface IApproachable
{
    /// <summary>
    /// Unit move towards target
    /// </summary>
    /// <param name="target">Target vector</param>
    void Approach(Vector2 target);
}