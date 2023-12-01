using UnityEngine;
public class Node
{
    public Vector2 Position { get; set; }
    public float Cost { get; set; }
    public float Heuristic { get; set; }
    public float TotalCost { get { return Cost + Heuristic; } }
    public Node Parent { get; set; }

    public Node(Vector2 position)
    {
        Position = position;
    }
}