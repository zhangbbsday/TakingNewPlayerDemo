using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteNodeButton : MenuButton
{
    private bool IsSelecting { get; set; }
    private float SelectRange { get; } = 0.5f;

    protected override void Update()
    {
        base.Update();
        DeleteNode();
    }

    private void DeleteNode()
    {
        if (!IsSelecting)
            return;

        Node node = FindNearestNode();
        if (node == null)
            return;

        if (Input.GetMouseButtonDown(0) && !MouseUtils.IsMouseOverUIObject())
            DeleteOne(node);
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

        Debug.Log($"{nearest.Key} : {nearest.Value}");
        if (nearest.Value < SelectRange)
            return nearest.Key;
        return null;
    }

    private void DeleteOne(Node node)
    {
        GameManager.Instance.NodesManager.DeleteNode(node);
        IsSelecting = false;
    }

    public override void PressAction()
    {
        IsSelecting = true;
    }

    public override void ReleseAction()
    {
        IsSelecting = false;
    }
}
