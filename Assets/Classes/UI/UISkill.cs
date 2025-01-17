﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UISkill : MonoBehaviour {
	public int skillNo;

	private SkillSet skillSet;
	private Text skillNameText;

	void Start() {
		skillSet = Player.instance.skillSet;
		skillNameText = GetComponentInChildren<Text>();
		if (skillSet.skills[skillNo] == null)
			return;
		GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>
			("Icons/" + skillSet.skills[skillNo].name + "_icon");
		skillNameText.text = skillSet.skills[skillNo].name + " x" + skillSet.skills[skillNo].charges;
        skillNameText.color = Color.white;
    }

	void Update() {
		//skillNameText.color = skillSet.selectedSkill == skillNo ? Color.red : Color.white;
		if (skillNo < skillSet.skills.Length && skillSet.skills[skillNo] != null)
			skillNameText.text = skillSet.skills[skillNo].name + " x" + skillSet.skills[skillNo].charges;
	}
}
