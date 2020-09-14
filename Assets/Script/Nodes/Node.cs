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

    public void Init(int id, Vector2 pos, NodeType type)
    {
        Id = id;
        Position = pos;
        Type = type;
    }

    protected override void Start()
    {
        base.Start();
        Position = Transform.position;
    }

    protected override void Update()
    {
        base.Update();

        Transform.position = Position;
    }
}
