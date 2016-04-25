using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UISkill : MonoBehaviour {
	public int skillNo;

	private SkillSet skillSet;
	private Text skillNameText;

	void Start() {
		skillSet = Player.instance.skillSet;
		skillNameText = GetComponentInChildren<Text>();
		GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>
			("Icons/" + skillSet.skills[skillNo].name + "_icon");
		skillNameText.text = skillSet.skills[skillNo].name + " x" + skillSet.skills[skillNo].charges;
	}

	void Update() {
		skillNameText.color = skillSet.selectedSkill == skillNo ? Color.red : Color.white;
		if (skillNo < skillSet.skills.Length)
			skillNameText.text = skillSet.skills[skillNo].name + " x" + skillSet.skills[skillNo].charges;
	}
}
