using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Smoke : MonoBehaviour
{
    private const float MAX_DURATION = 10;

    private float duration;
    private Image image;
    private Image fill;

    void Start()
    {
        duration = MAX_DURATION;
        image = GetComponent<Image>();
        fill = transform.GetChild(0).GetComponent<Image>();
    }

    void Update()
    {
        fill.fillAmount = duration / MAX_DURATION;
        if (duration <= 0)
        {
            //Player.instance.darkness.material.SetColor("_TintColor", new Color(0, 0, 0, 1f));
            Color c = Player.instance.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color;
            Player.instance.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, 1f);

            Player.instance.gameObject.layer = 10; // Player Layer
            Destroy(gameObject);
            return;
        }
        duration -= Time.deltaTime;
        //Player.instance.darkness.material.SetColor("_TintColor", new Color(0, 1, 0, 0.3f));
    }
}
