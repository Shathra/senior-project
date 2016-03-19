using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AIManager : MonoBehaviour{

    protected Graph levelGraph;
    protected List<Enemy> enemies;
    protected Player player;

    public void Awake() {

        AIController.Init(this);
        levelGraph = new Graph();
        enemies = new List<Enemy>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        contructGraph();
        setEnemies();
    }

    internal Player GetPlayer() {
        throw new NotImplementedException();
    }

    public void Start() {

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

    public Graph GetGraph() {

        return levelGraph;
    }

    public List<Node> GetPath( Vector2 source, Vector2 target) {

        return levelGraph.ShortestPath(source, target);
    }

    public List<Enemy> GetEnemies() {

        return enemies;
    }
}