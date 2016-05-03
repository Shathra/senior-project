using UnityEngine;
using System.Collections;

public class MainCameraController : MonoBehaviour
{
    public static MainCameraController instance { get; private set; }
    Vector3 mouseRelativeToPlayer;
    Vector3 targetPos;
    public Vector2 lastPosition { get; set; }

    private LineRenderer line;
    
    void Awake()
    {
        MainCameraController.instance = this;
    }
    void Start()
    {
        lastPosition = transform.position;
        line = GetComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Particles/Additive"));
        Color c = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        line.SetColors(c,c);
        line.SetWidth(0.1F, 0.1F);
        line.SetVertexCount(2);
    }



    void Update () {
        if(XInputTestCS.state.Buttons.RightShoulder == 0)
        {
            line.enabled = true;
        }
        else
        {
            line.enabled = false;
        }
        if (!Player.instance.gamePadActive)
        {
            mouseRelativeToPlayer = Camera.main.ScreenToWorldPoint(Input.mousePosition)
                - (Vector3)Player.instance.midPoint;
        }
        else
        {
            mouseRelativeToPlayer = Camera.main.ScreenToWorldPoint(new Vector3(XInputTestCS.state.ThumbSticks.Right.X*720, XInputTestCS.state.ThumbSticks.Right.Y*540, 0f))
               - (Vector3)Player.instance.midPoint + new Vector3(7f,3.3f,0f);
        }
        mouseRelativeToPlayer = new Vector3(mouseRelativeToPlayer.x, mouseRelativeToPlayer.y, transform.position.z);
        targetPos = Vector3.Lerp(new Vector3(0, 0, transform.position.z), mouseRelativeToPlayer,
            (Input.GetKey(KeyCode.Mouse1)|| XInputTestCS.state.Buttons.LeftShoulder == 0)? 0.5f : 0.25f);
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, 0.3f);
        
        /*Color color = Color.green;
        line.SetColors(color, new Color(color.r, color.g, color.b, 0.5f));*/

    }

    void LateUpdate()
    {
        Vector2 pos = new Vector2(XInputTestCS.state.ThumbSticks.Right.X * 720, XInputTestCS.state.ThumbSticks.Right.Y * 540);
        line.SetPosition(0, Player.instance.midPoint);
        line.SetPosition(1, pos);
        Vector2 heading = pos - Player.instance.midPoint;
        float distance = Vector2.Distance(pos, Player.instance.midPoint);

        RaycastHit2D obs = Physics2D.Raycast(Player.instance.midPoint, heading/distance, 100f, LayerMask.GetMask("Obstacle"));
        if (obs.collider != null)
            line.SetPosition(1, obs.point);
        
        lastPosition = transform.position;
    }
}
