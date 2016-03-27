using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// It defines spot abiilty. That's classes implementing this interface can spot. (TODO: Is name bad? cuz it actually defines units which can spot not ones that other units can spot to)
/// </summary>
public interface ISpotable
{
    /// <summary>
    /// Defines what happens after an object is spotted
    /// </summary>
    /// <param name="obj">Spotted object</param>
    void Spot(GameObject obj);
    void SpotOut(GameObject obj);
}