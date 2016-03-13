using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Test : MonoBehaviour{

    void Start() {

        Graph g = new Graph();
        Node a = (Node)gameObject.AddComponent( typeof(Node));
        Node b = (Node)gameObject.AddComponent(typeof(Node));
        Node c = (Node)gameObject.AddComponent(typeof(Node));
        Node d = (Node)gameObject.AddComponent(typeof(Node));

        a.edges = new Node[2];
        a.weights = new float[2];
        a.edges[0] = c;
        a.edges[1] = d;
        a.weights[0] = 3;
        a.weights[1] = 0;

        b.edges = new Node[2];
        b.weights = new float[2];
        b.edges[0] = a;
        b.edges[1] = d;
        b.weights[0] = -2;
        b.weights[1] = 1;

        c.edges = new Node[1];
        c.weights = new float[1];
        c.edges[0] = d;
        c.weights[0] = 5;

        d.edges = new Node[1];
        d.weights = new float[1];
        d.edges[0] = b;
        d.weights[0] = 4;

        g.AddNode(a);
        g.AddNode(b);
        g.AddNode(c);
        g.AddNode(d);

        float[,] res = g.CalculateShortestDistanceOfEachPair();
        for( int i = 0; i <4;i++)
            Debug.Log(res[i,0] + " " + res[i, 1] + " " + res[i, 2] + " " + res[i, 3]);
    }
}
