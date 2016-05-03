using UnityEngine;
using System.Collections;
using System;

public class SmokeSkill : Skill
{
    GameObject smokePrefab;

    public SmokeSkill() : base()
    {
        charges = 1;
        name = "Smoke";
        description = "Become invisible to" + 
            "enemies for a few seconds";
        smokePrefab = Resources.Load<GameObject>("Smoke");
    }

    public override void Cast(Vector2 target)
    {
        Color c = Player.instance.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color;
        Player.instance.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(c.r,c.g, c.b, 0.5f);
        GameObject smoke = GameObject.Instantiate(smokePrefab);
        Player.instance.gameObject.layer = 16; // Invisible
        /* GameObject nightVision = GameObject.Instantiate(nightVisionPrefab);*/
        smoke.transform.parent = UIController.canvas.transform;
        smoke.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100,0);
        GameObject.Instantiate(Resources.Load("SmokeAtma"), Player.instance.midPoint, Quaternion.identity);
    }
}
