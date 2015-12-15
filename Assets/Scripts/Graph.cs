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

        
        public void ShortestPath()
        {

        }


    }
}