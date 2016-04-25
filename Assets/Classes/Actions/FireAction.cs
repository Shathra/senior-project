using UnityEngine;
using System.Collections;

public class FireAction : Action
{
    private Player target;
    private float fireTime;

    public FireAction(Player player) : base(Action.PRIORITY_HOSTILE)
    {
        this.target = player;
        fireTime = 0;
    }

    public override void Execute(Enemy enemy)
    {
        fireTime -= Time.deltaTime;
        if( fireTime < 0) {
            ((Guardian)enemy).Fire(target.midPoint);
            fireTime = MLLevelStats.GetStat(LevelStat.GuardianWeaponFireRate);
        }
        done = PlayerGhost.instance.isActive;
    }
}
