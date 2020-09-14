using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPathButton : MenuButton
{
    private bool IsPlacing { get; set; }
    private Node StartNode { get; set; }
    private Node EndNode { get; set; }
    private float SelectRange { get; } = 0.5f;

    protected override void Update()
    {
        base.Update();
        SetPath();
    }


    private void SetPath()
    {
        if (!IsPlacing)
            return;

        Node node = FindNearestNode();
        if (node == null)
            return;

        if (Input.GetMouseButtonDown(0) && !MouseUtils.IsMouseOverUIObject())
            SetNode(node);
    }

    private void SetNode(Node node)
    {
        if (StartNode == null || StartNode == node)
        {
            StartNode = node;
            return;
        }

        EndNode = node;
        CreatePath();
        IsPlacing = false;
    }

    private void CreatePath()
    {
        GameManager.Instance.LinksManager.CreateLink(StartNode, EndNode);
    }

    private Node FindNearestNode()
    {
        Node[] nodes = GameManager.Instance.NodesManager.GetNodes();
        if (nodes == null || nodes.Length == 0)
            return null;

        KeyValuePair<Node, float> nearest = new KeyValuePair<Node, float>(nodes[0], float.MaxValue);
        foreach (var n in nodes)
        {
            float distance = Vector2.Distance(n.Position, MouseUtils.MouseWorldPosition);
            if (distance < nearest.Value)
                nearest = new KeyValuePair<Node, float>(n, distance);
        }

        if (nearest.Value < SelectRange)
            return nearest.Key;
        return null;
    }

    public override void PressAction()
    {
        IsPlacing = true;

        StartNode = null;
        EndNode = null;
    }

    public override void ReleseAction()
    {
        IsPlacing = false;

        StartNode = null;
        EndNode = null;
    }
}
