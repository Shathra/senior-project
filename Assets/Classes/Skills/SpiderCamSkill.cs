using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class SpiderCamSkill : Skill
{

    private GameObject spiderCamPrefab;
    Vector2 direction;
    Rigidbody2D spiderCam;

    public SpiderCamSkill() : base()
    {
        charges = 10;//Mathf.RoundToInt(MLLevelStats.GetStat(LevelStat.ShurikenLimit));
        name = "SpiderCam";
        description = "A tool to track places " +
            "that is out of your field of view.";
        spiderCamPrefab = Resources.Load<GameObject>("SpiderCam");
    }

    public override void Cast(Vector2 target)
    {
        direction = target - player.midPoint;
        spiderCam = ((GameObject)MonoBehaviour.Instantiate(spiderCamPrefab,
            player.midPoint, Quaternion.identity)).GetComponent<Rigidbody2D>();
        spiderCam.AddForce(direction.normalized * 80);
    }
}
