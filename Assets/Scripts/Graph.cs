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
        //  Get nodes out of the shapes in the scene
        GenerateNodes();
        //  Calculate each edge according to the relative time it
        //  takes to traverse the path for vertical ones
    }
    public void GenerateNodes()
    {
        //for each surface object in the scene create a vertex out of each stored vertex
        //if there is a ladder on the surface it is also a vertex or if a surface
        //is projecting on this it is also a vertex
            
        //in the surface object create a path from start to end covering all vertexes
            
        // after each node is generated create vertical edge according to the ladders 
        // and the height of the projections
        // directed downwards if the length is too high
        // undirected if ladder or projection is small height
    }

        
    public List<Node> ShortestPath( Node source, Node target)
    {
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
                float neighborDistance = 0;
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
