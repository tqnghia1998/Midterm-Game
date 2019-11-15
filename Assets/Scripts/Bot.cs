using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bot : Movement
{
    Rigidbody2D rb2d;
    float horizontal, vertical;
    BulletManager bullet;

    [SerializeField]
    LayerMask blockingLayer;
    enum Direction { Up, Down, Left, Right };

    public static bool freezing = false;
    public List<Vector2> path;
    public Vector2 destination;

    public bool flag = false;

    public void ToFreezeTank()
    {
        StopAllCoroutines();
    }

    public void ToUnfreezeTank()
    {
        isMoving = false;
    }

    public void FindDestination(string tankTag)
    {
        string targetName;

        if (tankTag == "BigTank")
        {
            targetName = "Eagle";
        }
        else
        {
            targetName = "UserTank";
        }

        GameObject target = GameObject.FindGameObjectWithTag(targetName);
        destination.x = (int)Mathf.Floor(target.transform.position.x);
        destination.y = (int)Mathf.Floor(target.transform.position.y);
    }

    public void DrawPath()
    {
        Tilemap map = GameObject.Find("Map").GetComponent<Tilemap>();
        TileBase original = map.GetTile(new Vector3Int(-13, -13, 0));

        for (int i = -13; i < 13; i++)
        {
            for (int j = -13; j < 13; j++)
            {
                map.SetTile(new Vector3Int(i, j, 0), original);
            }
        }

        for (int i = 0; i < path.Count; i++)
        {
            map.SetTile(new Vector3Int((int)path[i].x - 13, (int)path[i].y - 13, 0), null);
        }
    }

    public void FindPath()
    {
        string tankTag = gameObject.tag;

        FindDestination(tankTag);
        AStar._ReadFromGrid(tankTag);

        int currX = (int)Mathf.Floor(transform.position.x) + 13;
        int currY = (int)Mathf.Floor(transform.position.y) + 13;

        int tarrX = (int)destination.x + 13;
        int tarrY = (int)destination.y + 13;

        var tempPath = AStar._PathFinding(AStar.nodeData[currX, currY], AStar.nodeData[tarrX, tarrY], tankTag);
        
        if (tempPath.Count > 0)
        {
            tempPath.RemoveAt(tempPath.Count - 1);
            path = tempPath;
        }
        
        if (GameManager.isShowRoute == true)
        {
            DrawPath();
        }
        
        MoveByPath();
    }

    void Start()
    {
        bullet = GetComponentInChildren<BulletManager>();
        rb2d = GetComponent<Rigidbody2D>();
        path = new List<Vector2>();

        InvokeRepeating("AutoFire", 2, 1f);
        InvokeRepeating("FindPath", 2, 1f);
        InvokeRepeating("AutoCorrectPosition", 5, 5f);
    }

    void AutoFire()
    {
        if (!Bot.freezing)
        {
            bullet.Fire();
        }
    }

    private void AutoCorrectPosition()
    {
        float currX = transform.position.x;
        float currY = transform.position.y;

        // Round to nearest .5
        float willX = (float)(Mathf.Round(currX * 2) / 2);
        float willY = (float)(Mathf.Round(currY * 2) / 2);

        if (willX <= -12.5f)
        {
            willX = -12.5f;
        }

        if (willX >= 12.5f)
        {
            willX = 12.5f;
        }

        if (willY <= -12.5f)
        {
            willY = -12.5f;
        }

        if (willY >= 12.5f)
        {
            willY = 12.5f;
        }

        transform.position = new Vector2(willX, willY);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb2d != null)
        {
            rb2d.velocity = Vector3.zero;
            AutoCorrectPosition();
        }
    }

    protected override void MoveByPath()
    {
        if (path.Count > 0 && !isMoving && !Bot.freezing)
        {
            int currX = (int)Mathf.Floor(transform.position.x);
            int currY = (int)Mathf.Floor(transform.position.y);

            int nextX = (int)path[path.Count - 1].x - 13;
            int nextY = (int)path[path.Count - 1].y - 13;

            if (nextX == currX)
            {
                vertical = nextY > currY ? 1: -1;
                StartCoroutine(MoveVertical(vertical, rb2d));
            }
            else if (nextY == currY)
            {
                horizontal = nextX > currX ? 1: -1;
                StartCoroutine(MoveHorizontal(horizontal, rb2d));
            }

            path.RemoveAt(path.Count - 1);
        }
    }
}
