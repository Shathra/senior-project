using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {
    public int[]    properties; //Just in case we add other properties than vector3 distance
    public Node[]   edges;
    public float[]  weights;  //edges and weights must be equal in size
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void AddEdge(Node n)
    {

    }

    public override string ToString() {

        return "Node " + transform.position;
    }
}
