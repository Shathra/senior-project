using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {
    public int[]    properties; //Just in case we add other properties than vector3 distance
    public Node[]   edges;
    public float[]  weights;  //edges and weights must be equal in size

    public void AddEdge(Node n)
    {

    }

    public override string ToString() {

        return "Node " + transform.position;
    }

	public void OnDrawGizmos() {
		Color prevColor = Gizmos.color;
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(transform.position, 0.25f);
		Gizmos.color = prevColor;
	}
}
