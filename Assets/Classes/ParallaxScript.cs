using UnityEngine;
using System.Collections;

public class ParallaxScript : MonoBehaviour {

    float displacement;
    float yDisplacement;

    public float parallaxCoefficent;
    
	// Update is called once per frame
	void LateUpdate () {
        displacement = MainCameraController.instance.lastPosition.x - MainCameraController.instance.transform.position.x;
        yDisplacement = MainCameraController.instance.lastPosition.y - MainCameraController.instance.transform.position.y;
        transform.Translate(new Vector3(displacement/(parallaxCoefficent*10), yDisplacement, 0));
    }
}