using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : GameActor
{
    public int Id { get; private set; }
    private LineRenderer LineRenderer { get; set; }
    private Node LeftNode { get; set; }
    private Node RightNode { get; set; }

    public void Init(int id, Node left, Node right)
    {
        Id = id;
        LeftNode = left;
        RightNode = right;
    }

    protected override void Start()
    {
        base.Start();

        LineRenderer = GetComponent<LineRenderer>();

        LineRenderer.SetPosition(0, LeftNode.Position);
        LineRenderer.SetPosition(1, RightNode.Position);
    }

    public KeyValuePair<Node, Node> GetNodes()
    {
        return new KeyValuePair<Node, Node>(LeftNode, RightNode);
    }
}
