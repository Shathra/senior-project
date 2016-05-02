using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UISelectedSkıll : MonoBehaviour {
	public Text description;

	private SkillSet skillSet;
	private int cursor;

	void Start() {
		skillSet = Player.instance.skillSet;
	}

	void Update() {
		Vector2 mousePos = new Vector2(Input.mousePosition.x - Screen.width / 2,
			Input.mousePosition.y - Screen.height / 2);
		float angle = Vector2.Angle(Vector2.up, mousePos.normalized);
		if (mousePos.x < 0)
			angle = 360 - angle;
		cursor = Mathf.RoundToInt(angle) / 60;
		transform.rotation = Quaternion.Lerp(transform.rotation,
			Quaternion.Euler(0, 0, 360 - (60*cursor)),
			0.5f);
		description.text = "";
		if (skillSet.skills.Length > cursor && skillSet.skills[cursor] != null)
			description.text = skillSet.skills[cursor].description;

		if (Input.GetKeyUp(KeyCode.Tab))
			skillSet.selectedSkill = cursor;

		if (Input.GetKeyDown(KeyCode.Alpha1))
			skillSet.selectedSkill = 0;
		else if (Input.GetKeyDown(KeyCode.Alpha2))
			skillSet.selectedSkill = 1;
		else if (Input.GetKeyDown(KeyCode.Alpha3))
			skillSet.selectedSkill = 2;
		else if (Input.GetKeyDown(KeyCode.Alpha4))
			skillSet.selectedSkill = 3;
		else if (Input.GetKeyDown(KeyCode.Alpha5))
			skillSet.selectedSkill = 4;
		else if (Input.GetKeyDown(KeyCode.Alpha6))
			skillSet.selectedSkill = 5;
	}
}
