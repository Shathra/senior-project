using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    protected List<Node> nodes;
        
    public Graph() {
        nodes = new List<Node>();
    }

    public void AddNode( Node node) {

        nodes.Add(node);
    }

    public void CreateGraph() {

        //Calculate 2D distances of each node
        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].weights = new float[nodes[i].edges.Length];
            for (int j = 0; j < nodes[i].edges.Length; j++)
            {
                nodes[i].weights[j] = Vector3.Distance(nodes[i].transform.position, nodes[i].edges[j].transform.position);
            }
        }
    }

    public List<Node> ShortestPath(Vector2 source, Vector2 target) {

        Node sourceNode = GetNearestNode( source);
        Node targetNode = GetNearestNode( target);

        return ShortestPath(sourceNode, targetNode);
    }


    /// <summary>
    /// TODO-Selçuk:Not finished
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public List<Node> ShortestPath( Node source, Node target) {
        List<int> prev = new List<int>();
        List<float> distance = new List<float>();

        List<Node> remaining = new List<Node>();

        for( int i = 0; i < nodes.Count; i++) {

            distance.Add(float.MaxValue);
            remaining.Add(nodes[i]);
            prev.Add(-1);
        }

        int current = nodes.IndexOf(source);
        distance[current] = 0;
        remaining[current] = null;

        for( int i = 1; i < nodes.Count; i++) {

            for (int j = 0; j < nodes[current].edges.Length; j++) {

                int toIndex = nodes.IndexOf(nodes[current].edges[j]);
                if (distance[current] + nodes[current].weights[j] < distance[toIndex]) {

                    distance[toIndex] = distance[current] + nodes[current].weights[j];
                    prev[toIndex] = current;
                }
            }

            float min = float.MaxValue;
            int next = -1;
            for( int j = 0; j < remaining.Count; j++) {

                if( remaining[j] != null) {

                    if( distance[j] < min) {

                        min = distance[j];
                        next = j;
                    }
                }
            }

            if( next != -1) {

                current = next;
                remaining[next] = null;
            }
        }

        List<Node> path = new List<Node>();
        int currentIndex = nodes.IndexOf(target);
        int sourceIndex = nodes.IndexOf(source);
        path.Add(target);
        while( currentIndex != sourceIndex) {

            path.Add(nodes[prev[currentIndex]]);
            currentIndex = prev[currentIndex];
        }

        path.Reverse();
        return path;
    }

    public Node GetNearestNode(Vector2 pos) {

        if (nodes.Count == 0)
            return null;

        Node nodeToReturn = nodes[0];
        float minDistance = Vector2.Distance(pos, nodeToReturn.transform.position);
        float distance;
        for( int i = 1; i < nodes.Count; i++) {

            distance = Vector2.Distance(nodes[i].transform.position, pos);
            if( distance < minDistance) {

                minDistance = distance;
                nodeToReturn = nodes[i];
            }
        }

        return nodeToReturn;
    }
}
