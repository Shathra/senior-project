using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// It defines fire ability. That's classes implementing this interface can fire. (TODO: Is name bad? cuz it actually defines units which can fire not ones that other mobile units can fire to)
/// </summary>
public interface IFirable
{
    /// <summary>
    /// Fires to target
    /// </summary>
    /// <param name="target">Target player</param>
    void Approach(Player target);
}