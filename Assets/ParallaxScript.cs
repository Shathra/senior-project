﻿using UnityEngine;
using System.Collections;

public class ParallaxScript : MonoBehaviour {

    float displacement;
    float yDisplacement;

    public float parallaxCoefficent;
    
    // Use this for initialization
    void Start () {
	    
	}
    
	// Update is called once per frame
	void Update () {
        displacement = Player.instance.lastPosition.x - Player.instance.transform.position.x;
        yDisplacement = Player.instance.lastPosition.y - Player.instance.transform.position.y;
        transform.Translate(new Vector3(displacement/(parallaxCoefficent*5), yDisplacement,0));
    }
}