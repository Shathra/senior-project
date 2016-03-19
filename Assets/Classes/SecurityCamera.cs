using UnityEngine;
using System.Collections;

/// <summary>
/// Immobile unit which alerts other enemy in a range when it sees a player
/// </summary>
public class SecurityCamera : Enemy {
    private const float ROTATION_AMOUNT = 85;
    public const float RANGE = 10;

    public float visionAngle { get; set; }

    private SecurityCameraVision vision;
    private MeshFilter visionMesh;
    private PolygonCollider2D visionCollider;

    private bool direction;

    void Start() {
        direction = false;
        visionAngle = 30;
        vision = GetComponentInChildren<SecurityCameraVision>();
        visionMesh = vision.gameObject.GetComponent<MeshFilter>();
        visionCollider = vision.gameObject.GetComponent<PolygonCollider2D>();
        GenerateVision();
    }

    void FixedUpdate() {
        transform.eulerAngles = new Vector3(0, 0,
            Mathf.MoveTowards(transform.eulerAngles.z, direction ? 180 - ROTATION_AMOUNT : 180 + ROTATION_AMOUNT, Time.fixedDeltaTime * 40));
        if (Mathf.Abs(transform.eulerAngles.z - (180 - ROTATION_AMOUNT)) < 1)
            direction = false;
        else if (Mathf.Abs(transform.eulerAngles.z - (180 + ROTATION_AMOUNT)) < 1)
            direction = true;
    }


    public void GenerateVision() {
        Vector2[] points = new Vector2[3];
        points[0] = Vector2.zero;
        float x = Mathf.Sin((visionAngle / 2) * Mathf.Deg2Rad) * RANGE;
        float y = Mathf.Cos((visionAngle / 2) * Mathf.Deg2Rad) * RANGE; ;
        points[1] = new Vector2(-x, y);
        points[2] = new Vector2(x, y);
        visionCollider.points = points;
        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[] { (Vector3)points[0], (Vector3)points[1], (Vector3)points[2] };
        mesh.triangles = new int[] { 0, 1, 2 };
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        visionMesh.mesh = mesh;
    }

    public void Spot(Player player) {
        GameObject nearestGuard = NearestGuard();
        nearestGuard.GetComponent<Guardian>().Spot(player.gameObject);
    }
    private GameObject NearestGuard()
    {
        GameObject enemies = GameObject.Find("Enemies");
        GameObject enemy = enemies.transform.GetChild(0).gameObject;
        float smallestDistance = Mathf.Abs(Vector2.Distance(enemy.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position));
        for (int i = 1; i < enemies.transform.childCount; i++)
        {
            float distance = Mathf.Abs(Vector2.Distance(enemies.transform.GetChild(i).position, GameObject.FindGameObjectWithTag("Player").transform.position));
            if ( distance < smallestDistance)
            {
                smallestDistance = distance;
                enemy = enemies.transform.GetChild(i).gameObject;
            }
        }
        return enemy;
    }
}
