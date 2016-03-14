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

    /// <summary>
    /// Deep copies a graph
    /// </summary>
    /// <returns>Returns copied graph</returns>
    public Graph DeepCopy() {

        //TODO: We will need this method, complete it.
        Graph graph = new Graph();

        return graph;
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

    /// <summary>
    /// A sufficient solution to vehicle routing problem used to search graph by guardians.
    /// </summary>
    /// <param name="graph">Graph which should be traversed by searchers</param>
    /// <param name="searchers">Searchers nodes which searcher start searching. Note: Searcher nodes should be in graph</param>
    /// <returns>Returns list of paths which searchers should traverse on.</returns>
    public static List<List<Node>> SearchGraph( Graph graph, List<Node> searchers) {

        //TODO:Finish the function and test it
        List<Node> visited = new List<Node>();
        List<Node> unvisited = new List<Node>();
        List<Node> nodes = graph.nodes;
        int noNodes = nodes.Count;
        int noGuardians = searchers.Count;
        int[] nodeMarks = new int[noNodes];
        int[] guardianMarks = new int[noGuardians];
        float[] guardianTimes = new float[noGuardians];
        List<Node> guardianCurrentNodes = new List<Node>();

        float[,] distance = graph.CalculateShortestDistanceOfEachPair();
        float[,] distance_searcher = new float[noGuardians, noNodes];
        for( int i = 0; i < searchers.Count; i++) {

            int index = graph.nodes.IndexOf(searchers[i]);
            for (int j = 0; j < noNodes; j++)
                distance_searcher[i, j] = distance[index, j];
        }

        List<List<Node>> result = new List<List<Node>>();

        for( int i = 0; i < noNodes; i++) {

            unvisited.Add(graph.nodes[i]);
            nodeMarks[i] = 0;
        }

        for( int i = 0; i < noGuardians; i++) {

            guardianMarks[i] = 0;
            guardianTimes[i] = 0;
            guardianCurrentNodes.Add(searchers[i]);
            result[i] = new List<Node>();
        }

        for( int i = 0; i < noGuardians; i++) {

            //Find fartest unvisited node to guardians
            if (unvisited.Count == 0)
                break;

            Node farNode = unvisited[0];
            int index = graph.nodes.IndexOf(farNode);
            IntFloatPair guardianDistancePair = Utils.MinColumn(distance_searcher, index);
            for ( int j = 1; j < unvisited.Count; j++) {

                Node current_node = unvisited[j];
                index = graph.nodes.IndexOf(current_node);

                IntFloatPair currentPair = Utils.MinColumn(distance_searcher, index);
                if( currentPair.Value >guardianDistancePair.Value) {

                    farNode = current_node;
                    guardianDistancePair = currentPair;
                }
            }

            //Fartest node found, send closest guardian
            List<Node> path = null;
            path = graph.ShortestPath(searchers[guardianDistancePair.Index], farNode);
            result[guardianDistancePair.Index] = path;
            for (int j = 0; j < noNodes; j++)
                distance_searcher[guardianDistancePair.Index, j] = Single.PositiveInfinity;
            for( int j = 0; j < path.Count; j++) {

                if( unvisited.Contains( path[j])) {

                    unvisited.Remove(path[j]);
                    visited.Add(path[j]);
                }
            }
            guardianTimes[guardianDistancePair.Index] = guardianDistancePair.Value;
            guardianCurrentNodes[guardianDistancePair.Index] = farNode;
        }

        //While there is a unvisited node in the graph
        while( unvisited.Count != 0) {

            //Find guy who finish its job earliest and assign this node to him
            int freeGuard = 0;
            float freeTime = guardianTimes[0];
            for ( int i = 1; i < noGuardians; i++) {

                if( freeTime > guardianTimes[i]) {

                    freeGuard = i;
                    freeTime = guardianTimes[i];
                }
            }

            //Free guy found, assign this node to him.
            float guardianToNodeDistance = distance[graph.nodes.IndexOf(guardianCurrentNodes[freeGuard]), graph.nodes.IndexOf( unvisited[0])];
            List<Node> path = graph.ShortestPath(guardianCurrentNodes[freeGuard], unvisited[0]);
            path.RemoveAt(0);
            result[freeGuard].AddRange(path);
            for (int j = 0; j < path.Count; j++) {

                if (unvisited.Contains(path[j])) {

                    unvisited.Remove(path[j]);
                    visited.Add(path[j]);
                }
            }
            guardianTimes[freeGuard] += guardianToNodeDistance;
            guardianCurrentNodes[freeGuard] = unvisited[0];
        }

        return result;
    }

    /// <summary>
    /// Calculates shortest distance of each pair or nodes in the graph by using Floyd-Warshall algorithm.
    /// It is an O(n^3) algorithm, use it wisely.
    /// </summary>
    /// <returns>Returns table of distances, distance from i. node to j. node is represented by i. row and j. column</returns>
    public float[,] CalculateShortestDistanceOfEachPair() {

        int no_nodes = nodes.Count;
        float[,] prev_iteration = new float[no_nodes,no_nodes];
        float[,] current_iteration = null;

        for( int i = 0; i < no_nodes; i++) {

            for( int j = 0; j < no_nodes; j++)
                prev_iteration[i, j] = Single.PositiveInfinity;
            prev_iteration[i, i] = 0;

            for( int j = 0; j < nodes[i].edges.Length; j++) {

                int index = nodes.IndexOf(nodes[i].edges[j]);
                prev_iteration[i, index] = nodes[i].weights[j];
            }
        }

        //Iteration loop which changes current and prev iterations
        for( int i = 0; i < no_nodes; i++) {

            current_iteration = prev_iteration;
            //Can table be improved by visiting node i
            for( int j = 0; j < no_nodes; j++) {

                if (i == j)
                    continue;

                for( int k = 0; k < no_nodes; k++) {

                    if (prev_iteration[j, k] > prev_iteration[j, i] + prev_iteration[i, k])
                        current_iteration[j, k] = prev_iteration[j, i] + prev_iteration[i, k];
                }
            }
        }
        return current_iteration;
    }

    /// <summary>
    /// A utility function which calculates the distance of a path by adding weights of all edges in the path
    /// </summary>
    /// <param name="path">Path</param>
    /// <returns>Returns distance</returns>
    public static float CalculatePathDistance( List<Node> path) {

        float total = 0;
        for( int i = 0; i < path.Count - 1; i++) {

            int nextIndex = Array.IndexOf(path[i].edges, path[i + 1]);
            total += path[i].weights[nextIndex];
        }

        return total;
    }

    public List<Node> ShortestPath(Vector2 source, Vector2 target) {

        Node sourceNode = GetNearestNode( source);
        Node targetNode = GetNearestNode( target);

        return ShortestPath(sourceNode, targetNode);
    }


    /// <summary>
    /// Dijkstra algorithm
    /// </summary>
    /// <param name="source">Source node where a unit starts moving</param>
    /// <param name="target">Target node which unit tries to arrive</param>
    /// <returns>Returns list of nodes which unit should traverse in order to achieve target node with shorthest path</returns>
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

        Debug.Log(path.Count);
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
