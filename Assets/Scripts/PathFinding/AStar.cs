using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class Node
{
    public float f;
    public float g;
    public float h;
    public Vector2 pos;
    public Node parent;
    public int inside = 0;
}

public class AStar : MonoBehaviour
{
    public static Node[,] nodeData;
    public static int[,] gridData;
    public static Tilemap iron, brick, water;

    void Start()
    {
        _InitNodeData();
    }

    public static void _ReadFromGrid()
    {
        GameObject grid = GameObject.Find("Grid");
        iron = GameObject.Find("Iron").GetComponent<Tilemap>();
        brick = GameObject.Find("Brick").GetComponent<Tilemap>();
        water = GameObject.Find("Water").GetComponent<Tilemap>();

        for (int i = 0; i < 26; i++)
        {
            for (int j = 0; j < 26; j++)
            {
                AStar.gridData[i, j] = 0;
                AStar.nodeData[i, j] = new Node()
                {
                    f = 0,
                    g = 0,
                    h = 0,
                    pos = new Vector2(i, j),
                    parent = null,
                    inside = 0
                };

                Vector3 currentTile = new Vector3(i - 13, j - 13, 0);

                if (AStar.iron.GetTile(AStar.iron.WorldToCell(currentTile)) != null
                    || AStar.brick.GetTile(AStar.brick.WorldToCell(currentTile)) != null
                        || AStar.water.GetTile(AStar.water.WorldToCell(currentTile)) != null)
                {
                    if (((i == 11 || i == 14) && (j == 0 || j == 1 || j == 2)) || (j == 2 && (i == 12 || i == 13)))
                    {

                    }
                    else
                    {
                        AStar.gridData[i, j] = 1;
                    }
                }
            }
        }
    }

    public static void _InitNodeData()
    {
        AStar.nodeData = new Node[26, 26];
        AStar.gridData = new int[26, 26];

        for (int i = 0; i < 26; i++)
        {
            for (int j = 0; j < 26; j++)
            {
                AStar.nodeData[i, j] = new Node()
                {
                    f = 0,
                    g = 0,
                    h = 0,
                    pos = new Vector2(i, j),
                    parent = null,
                    inside = 0
                };
            }
        }

        _ReadFromGrid();
    }

    public static List<Vector2> _PathFinding(Node start, Node end)
    {
        List<Vector2> path = new List<Vector2>();

        // The set of nodes to be evaluated
        List<Node> close = new List<Node>();

        // The set of nodes already eveluated
        List<Node> open = new List<Node>();

        // Add the start node to open
        start.g = 0;
        start.h = AStar._Cost(start.pos, end.pos);
        start.f = start.g + start.h;
        open.Add(start);
        start.inside = 0;

        while (open.Count > 0)
        {
            Node current = AStar._MinF(open);

            open.Remove(current);
            current.inside = 0;

            close.Add(current);
            current.inside = 2;

            // Path has been found
            if (current.pos == end.pos)
            {
                // Debug.Log("FIND PATH SUCCESS");
                AStar._Show(path, current);

                break;
            }

            // For each in neighbour nodes
            for (int i = (int)current.pos.x - 1; i <= current.pos.x + 1; i++)
            {
                for (int j = (int)current.pos.y - 1; j <= current.pos.y + 1; j++)
                {
                    if (i < 0 || j < 0 || i >= 26 || j >= 26)
                    {
                        continue;
                    }

                    // Only get 4 neighbours
                    if (i == (int)current.pos.x - 1 && j == (int)current.pos.y - 1)
                    {
                        continue;
                    }

                    if (i == (int)current.pos.x - 1 && j == (int)current.pos.y + 1)
                    {
                        continue;
                    }

                    if (i == (int)current.pos.x + 1 && j == (int)current.pos.y - 1)
                    {
                        continue;
                    }

                    if (i == (int)current.pos.x + 1 && j == (int)current.pos.y + 1)
                    {
                        continue;
                    }

                    // If current tile is occupied
                    if (AStar.gridData[i, j] != 0)
                    {
                        continue;
                    }

                    // Setup node
                    AStar.nodeData[i, j].g = AStar._Cost(start.pos, AStar.nodeData[i, j].pos);
                    AStar.nodeData[i, j].h = AStar._Cost(AStar.nodeData[i, j].pos, end.pos);
                    AStar.nodeData[i, j].f = AStar.nodeData[i, j].g + AStar.nodeData[i, j].h;

                    // If it is in open set
                    if (AStar.nodeData[i, j].inside == 1)
                    {
                        if (AStar.nodeData[i, j].g > current.g + AStar._Cost(AStar.nodeData[i, j].pos, current.pos))
                        {
                            AStar.nodeData[i, j].g = current.g + AStar._Cost(AStar.nodeData[i, j].pos, current.pos);
                            AStar.nodeData[i, j].f = AStar.nodeData[i, j].g + AStar.nodeData[i, j].h;
                            AStar.nodeData[i, j].parent = current;
                        }
                    }
                    
                    // If it is not in anything
                    if (AStar.nodeData[i, j].inside == 0)
                    {
                        AStar.nodeData[i, j].g = current.g + AStar._Cost(AStar.nodeData[i, j].pos, current.pos);
                        AStar.nodeData[i, j].f = AStar.nodeData[i, j].g + AStar.nodeData[i, j].h;
                        AStar.nodeData[i, j].parent = current;
                        open.Add(AStar.nodeData[i, j]);
                        AStar.nodeData[i, j].inside = 1;
                    }

                    // If it is not in close set
                    if (AStar.nodeData[i, j].inside == 2)
                    {
                        if (AStar.nodeData[i, j].g > current.g + AStar._Cost(AStar.nodeData[i, j].pos, current.pos))
                        {
                            open.Remove(nodeData[i, j]);
                            nodeData[i, j].inside = 0;
                            close.Add(nodeData[i, j]);
                            nodeData[i, j].inside = 2;
                        }
                    }
                }
            }
        }

        if (path.Count == 0)
        {
            Debug.Log("THERE IS NO PATH FROM " + start.pos + " TO " + end.pos);
        }

        return path;
    }

    public static void _Show(List<Vector2> path, Node node)
    {
        if (node != null)
        {
            path.Add(node.pos);
            AStar._Show(path, node.parent);
        }
        else
        {
            
        }
    }

    public static Node _MinF(List<Node> open)
    {
        Node minF = open[0];
        
        for (int i = 1; i < open.Count; i++)
        {
            if (minF.f > open[i].f)
            {
                minF = open[i];
            }
        }

        return minF;
    }

    public static float _Cost(Vector2 a, Vector2 b)
    {
        return Mathf.Sqrt((b.x-a.x) * (b.x-a.x) + (b.y-a.y) * (b.y-a.y));
    }
}