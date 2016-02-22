using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    List<Node> nodes;
        
    public Graph()
    {
        CreateGraph();
    }

    public void CreateGraph()
    {
        //Calculate 2D distances of each node
        for (int i = 0; i < nodes.Count; i++)
        {
            for (int j = 0; j < nodes[i].edges.Length; j++)
            {
                nodes[i].weights[j] = Vector3.Distance(nodes[i].transform.position, nodes[i].edges[j].transform.position);
            }
        }
    }

    /// <summary>
    /// TODO-Selçuk:Not finished
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public List<Node> ShortestPath( Node source, Node target)
    {
        //TODO-Selcuk: It is not correct, I'll fix it asap.
        List<int> prev = new List<int>();
        List<float> distance = new List<float>();
        List<float> distanceTraversed = new List<float>();

        for( int i = 0; i < nodes.Count; i++)
        {
            distance.Add(float.MaxValue);
            distanceTraversed.Add(float.MaxValue);
            prev.Add(-1);
        }

        distance[nodes.IndexOf(source)] = 0;
        distanceTraversed[nodes.IndexOf(source)] = 0;

        for (int j = 0; j < nodes.Count; j++)
        {

            Node closest = null;
            int closestIndex = 0;
            float min = distanceTraversed[0];
            for( int i = 0; i < nodes.Count; i++)
            {
                if( distanceTraversed[i] < min)
                {
                    closest = nodes[i];
                    closestIndex = i;
                    min = distanceTraversed[i];
                }
            }
            distanceTraversed[closestIndex] = float.MaxValue;

            for( int i = 0; i < closest.edges.Length; i++)
            {
                int neighborIndex = nodes.IndexOf(closest.edges[i]);
                float neighborDistance = closest.weights[i];                // Imza:Firat (Bug cikarsa diye) 
                float alt = distance[closestIndex] + neighborDistance;
                if( alt < distance[neighborIndex])
                {
                    distance[neighborIndex] = alt;
                    prev[neighborIndex] = closestIndex;
                }
            }
        }

        List<Node> path = new List<Node>();
        int index = prev[nodes.IndexOf(target)];
        int sourceIndex = nodes.IndexOf(source);
        while( index != sourceIndex)
        {
            path.Add(nodes[prev[index]]);
            index = prev[index];
        }

        path.Reverse();
        return path;
    }
}
