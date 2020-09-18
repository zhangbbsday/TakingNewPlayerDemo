using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NodesManager
{
    public Transform NodesParent { get; private set; }
    private int GlobalIndex { get => globalIndex++; }
    private int globalIndex;
    private string NodesParentName { get; } = "Nodes";
    private Dictionary<int, Node> Nodes { get; } = new Dictionary<int, Node>();
    private Node StartNode { get; set; }
    private Node EndNode { get; set; }

    private static string[] NodesName { get; } =
{
        "Node",
        "StartNode",
        "EndNode",
    };


    public NodesManager()
    {
        FindParent();
        globalIndex = 0;
    }

    public Node CreateNode(Node.NodeType type, Vector2 position)
    {
        string nodeName = NodesName[(int)type];

        Node node = GameManager.Instance.ResourcesManager.GetNode(nodeName);
        node = GameObject.Instantiate(node, position, Quaternion.identity, NodesParent);
        node.Init(GlobalIndex, position, type);

        SetNode(node);

        return node;
    }

    public void DeleteNode(Node node)
    {
        ClearNode(node);
        GameObject.Destroy(node.GameObject);
    }

    public bool HasNodeTypeExisted(Node.NodeType type)
    {
        switch (type)
        {
            case Node.NodeType.NormalNode:
                return Nodes.Count != 0;
            case Node.NodeType.StartNode:
                return StartNode != null;
            case Node.NodeType.EndNode:
                return EndNode != null;
        }

        throw new System.Exception($"不存在{type}类型的节点!");
    }

    public Node[] GetNodes()
    {
        return Nodes.Values.ToArray();
    }

    public Node GetMouseNearestNode(float selectRange = 0.5f)
    {
        Node[] nodes = GetNodes();
        if (nodes == null || nodes.Length == 0)
            return null;

        KeyValuePair<Node, float> nearest = new KeyValuePair<Node, float>(nodes[0], float.MaxValue);
        foreach (var n in nodes)
        {
            float distance = Vector2.Distance(n.Position, MouseUtils.MouseWorldPosition);
            if (distance < nearest.Value)
                nearest = new KeyValuePair<Node, float>(n, distance);
        }

        if (nearest.Value < selectRange)
            return nearest.Key;
        return null;
    }

    private void SetNode(Node node)
    {
        Node temp = null;
        switch (node.Type)
        {
            case Node.NodeType.NormalNode:
                break;
            case Node.NodeType.StartNode:
                temp = StartNode;
                SetSpecialNode(node, ref temp);
                StartNode = temp;
                break;
            case Node.NodeType.EndNode:
                temp = EndNode;
                SetSpecialNode(node, ref temp);
                EndNode = temp;
                break;
        }
        Nodes.Add(node.Id, node);
    }

    private void SetSpecialNode(Node node, ref Node target)
    {
        if (target == null) 
        {
            target = node;
            return;
        }

        DeleteNode(target);
        target = node;
    }

    private void ClearNode(Node node)
    {
        switch (node.Type)
        {
            case Node.NodeType.NormalNode:
                break;
            case Node.NodeType.StartNode:
                StartNode = null;
                break;
            case Node.NodeType.EndNode:
                EndNode = null;
                break;
        }
        RemoveNode(node);
    }

    private void RemoveNode(Node node)
    {
        if (Nodes.ContainsKey(node.Id))
        {
            Nodes.Remove(node.Id);
        }
    }

    private void FindParent()
    {
        GameObject obj = GameObject.Find(NodesParentName);
        if (obj == null)
            obj = new GameObject(NodesParentName);

        NodesParent = obj.transform;
        NodesParent.transform.position = Vector2.zero;
    }
}
