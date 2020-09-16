using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : GameActor
{
    public int Id { get; private set; }
    public Vector2 Position { get; set; }
    private LineRenderer LineRenderer { get; set; }
    private Node LeftNode { get; set; }
    private Node RightNode { get; set; }

    public void Init(int id, Node left, Node right)
    {
        Id = id;
        LeftNode = left;
        RightNode = right;

        Position = (left.Position + right.Position) / 2;
    }

    protected override void Start()
    {
        base.Start();
        LineRenderer = GetComponent<LineRenderer>();

        LineRenderer.SetPosition(0, LeftNode.Position);
        LineRenderer.SetPosition(1, RightNode.Position);
    }

    protected override void Update()
    {
        base.Update();
        CheckNodesState();

        Transform.position = Position;
    }

    private void CheckNodesState()
    {
        if (LeftNode == null || RightNode == null)
        {
            GameManager.Instance.LinksManager.DeleteLink(this);
            return;
        }
    }

    public KeyValuePair<Node, Node> GetNodes()
    {
        return new KeyValuePair<Node, Node>(LeftNode, RightNode);
    }

    public void SelectEffect()
    {

    }
}
