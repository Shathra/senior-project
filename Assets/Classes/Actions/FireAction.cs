using UnityEngine;
using System.Collections;

public class FireAction : Action
{
    private Player target;

    public FireAction(Player player)
    {
        this.target = player;
    }

    public override void Execute(Enemy enemy)
    {
        
    }
}
