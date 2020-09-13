using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : GameActor
{
    public enum NodeType
    {
        NormalNode,
        StartNode,
        EndNode,
    }

    public int Id { get; private set; }
    public Vector2 Position { get; set; }
    public NodeType Type { get; private set; }
    private List<Node> Links { get; set; } = new List<Node>();

    protected override void Start()
    {
        base.Start();
    }

    public void Link(Node node)
    {
        if (HasAlreadyExists(node))
            return;

        Links.Add(node);
    }

    private bool HasAlreadyExists(Node node)
    {
        foreach (var n in Links)
        {
            if (n.Id == node.Id)
            {
                return true;
            }
        }

        return false;
    }
}
