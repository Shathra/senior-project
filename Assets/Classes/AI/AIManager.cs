using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AIManager : MonoBehaviour{

    protected Graph levelGraph;
    protected List<Guardian> guardians;
    protected Player player;

    public void Awake() {

        AIController.Init(this);
        levelGraph = new Graph();
        guardians = new List<Guardian>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        contructGraph();
        setGuardians();
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

    protected void setGuardians() {

        Transform guardiansObj = GameObject.Find("Guardians").transform;
        foreach (Transform child in guardiansObj)
            guardians.Add(child.GetComponent<Guardian>());
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

    public List<Guardian> GetGuardians() {

        return guardians;
    }
}