using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AIManager : MonoBehaviour{

    protected Graph levelGraph;
    protected List<Enemy> enemies;

    public void Start() {

        AIController.Init(this);
        levelGraph = new Graph();
        enemies = new List<Enemy>();

        contructGraph();
        setEnemies();

        //TESTING
        /*Debug.Log(GetNearestNode(new Vector2(-5, 5)));
        Debug.Log(GetNearestNode(new Vector2(-5, 0)));

        List<Node> path = levelGraph.ShortestPath(new Vector2(-5, 5), new Vector2(-5, 0));
        if( path != null)
            foreach (Node node in path)
                Debug.Log(node);*/
    }

    void OnDrawGizmos()
    {
        if (!Config.DebugMode)
            return;

        Transform graph = transform.Find("Graph");
        foreach (Transform child in graph) {

            Node node;
            node = child.GetComponent<Node>();

            for (int i = 0; i < node.edges.Length; i++)
                Gizmos.DrawLine(child.position, node.edges[i].transform.position);
        }
    }

    protected void setEnemies() {

        Transform enemiesObj = GameObject.Find("Enemies").transform;
        foreach (Transform child in enemiesObj)
            enemies.Add(child.GetComponent<Enemy>());
    }

    protected void contructGraph()
    {
        Transform graph = transform.Find("Graph");
        foreach (Transform child in graph) {

            levelGraph.AddNode(child.GetComponent<Node>());
            if (!Config.DebugMode)
                child.GetComponent<MeshRenderer>().enabled = false;
        }

        levelGraph.CreateGraph();
    }

    public Node GetNearestNode( Vector2 pos) {

        return levelGraph.GetNearestNode( pos);
    }

    public List<Node> GetPath( Vector2 source, Vector2 target) {

        return levelGraph.ShortestPath(source, target);
    }

    /*
    public void Test() {

        foreach (Enemy enemy in enemies) {
            Vector2 pos = enemy.transform.position;
            Debug.Log(pos);
            enemy.actionQueue.Insert(new ApproachAction(pos, new Vector2(2, 0)));
        }
    }
    */
}