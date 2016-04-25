using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(FOV2DEyes))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class FOV2DVisionCone : MonoBehaviour 
{
	public enum Status
	{
		Idle,
		Suspicious,  
		Alert
	}
	public Status status;
	public List<Material> materials;

    Vector3[] newVertices;
    Vector2[] newVertices2D;
    Vector2[] newUV;
    int[] newTriangles;
    Mesh mesh;
    MeshRenderer meshRenderer;
    FOV2DEyes eyes;
    List<RaycastHit2D> hits;
    Vision vision;

    int i;
    int v;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        meshRenderer = GetComponent<MeshRenderer>();
        eyes = gameObject.GetComponent<FOV2DEyes>();

        meshRenderer.material = materials[0];

        vision = gameObject.GetComponent<Vision>();
    }

    void LateUpdate()
    {
        UpdateMesh();
        UpdateCollider();
    }
    void UpdateCollider()
    {
        vision.visionCollider.points = newVertices2D;
    }


    void UpdateMesh()
    {
        hits = eyes.hits;

        if (hits == null || hits.Count == 0)
            return;

        if (mesh.vertices.Length != hits.Count + 1)
        {
            mesh.Clear();
            newVertices = new Vector3[hits.Count + 1];
            newVertices2D = new Vector2[hits.Count + 1];
            newTriangles = new int[(hits.Count - 1) * 3];

            i = 0;
            v = 1;
            while (i < newTriangles.Length)
            {
                if ((i % 3) == 0)
                {
                    newTriangles[i] = 0;
                    newTriangles[i + 1] = v;
                    newTriangles[i + 2] = v + 1;
                    v++;
                }
                i++;
            }
        }

        newVertices[0] = Vector3.zero;
        newVertices2D[0] = Vector2.zero;
        for (i = 1; i <= hits.Count; i++)
        {
            newVertices[i] = transform.InverseTransformPoint(hits[i - 1].point);
            //newVertices[i] = new Vector3(newVertices[i].x, newVertices[i].y, 1);
            newVertices2D[i] = transform.InverseTransformPoint(hits[i - 1].point);
        }

        newUV = new Vector2[newVertices.Length];
        i = 0;
        while (i < newUV.Length)
        {
            newUV[i] = new Vector2(newVertices[i].x, newVertices[i].z);
            i++;
        }

        mesh.vertices = newVertices;
        mesh.triangles = newTriangles;
        mesh.uv = newUV;

        mesh.normals = new Vector3[mesh.vertices.Length];
        for (int n = 0; n < mesh.vertices.Length; n++)
        {
            mesh.normals[n] = Vector3.forward;
        }

        mesh.RecalculateBounds();

    }

    void UpdateMeshMaterial()
    {
        for (i = 0; i < materials.Count; i++)
        {
            if (i == (int)status && meshRenderer.material != materials[i])
            {
                meshRenderer.material = materials[i];
            }
        }
    }
}
