using PathFinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStar : MonoBehaviour
{
    private List<Vector2> isEmptyList = new List<Vector2>();
    public List<Vector2> IsEmptyList
    {
        get => isEmptyList;
        set
        {
            isEmptyList = value;
        }
    }
    public Vector2 vecStart { get; set; }
    public Vector2 vecEnd { get; set; }
    private static AStar instance;
    public static AStar Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }
    public List<Vector2> FindPath()
    {
        var openList = new List<Node>();
        var closedList = new HashSet<Node>();
        var startNode = new Node(vecStart);
        var endNode = new Node(vecEnd);

        startNode.Cost = 0;
        startNode.Heuristic = GetDistance(startNode, endNode);
        openList.Add(startNode);

        while ( openList.Count>0)
        {
            var currentNode = openList.OrderBy(n => n.TotalCost).First();

            if (currentNode.Position == endNode.Position)
            {
                foreach (GridObject grid in UserManager.Instance.GridEmptyList)
                {
                    grid.ISSoldier = false;
                }
                return GetPath(currentNode);
                break;
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (var neighbor in GetNeighbors(currentNode))
            {
                if (closedList.Any(n => n.Position == neighbor.Position))
                    continue;

                var tentativeCost = currentNode.Cost + GetDistance(currentNode, neighbor);

                if (tentativeCost < neighbor.Cost || !openList.Contains(neighbor))
                {
                    neighbor.Cost = tentativeCost;
                    neighbor.Heuristic = GetDistance(neighbor, endNode);
                    neighbor.Parent = currentNode;

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }

        return null; // Yol bulunamazsa boþ liste dön

    }

    private List<Node> GetNeighbors(Node node)
    {
        var neighbors = new List<Node>();

        var directions = new Vector2[]
        {
              new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1),
              new Vector2(1, 1), new Vector2(-1, 1), new Vector2(1, -1), new Vector2(-1, -1)
        };

        foreach (var direction in directions)
        {
            var neighborPos = node.Position + direction;
            if (isEmptyList.Contains(neighborPos))
            {
                neighbors.Add(new Node(neighborPos));
            }
        }
        return neighbors;
    }

    private float GetDistance(Node a, Node b)
    {
        // Ýki düðüm arasýndaki mesafeyi hesaplayýn
        return Vector2.Distance(a.Position, b.Position);
    }

    private List<Vector2> GetPath(Node endNode)
    {
        List<Vector2> path = new List<Vector2>();
        Node current = endNode;
        while (current != null)
        {
            path.Add(current.Position);
            current = current.Parent;
        }
        path.Reverse();
        return path;
    }
}












