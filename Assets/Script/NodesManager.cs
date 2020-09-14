using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesManager
{
    public Transform NodesParent { get; private set; }
    private int GlobalIndex { get => globalIndex++; }
    private string NodesParentName { get; } = "Nodes";
    private Dictionary<int, Node> NormalNodes { get; } = new Dictionary<int, Node>();
    private Node StartNode { get; set; }
    private Node EndNode { get; set; }
    private int globalIndex;
    private static string[] NodesName { get; } =
{
        "Node",
        "StartNode",
        "EndNode",
    };


    public NodesManager()
    {
        FindParent();
    }

    public Node CreateNode(Node.NodeType type, Vector2 position)
    {
        string nodeName = NodesName[(int)type];

        Node node = GameManager.Instance.ResourcesManager.GetNode(nodeName);
        node = GameObject.Instantiate(node, position, Quaternion.identity, GameManager.Instance.NodesManager.NodesParent);
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
                return NormalNodes.Count != 0;
            case Node.NodeType.StartNode:
                return StartNode != null;
            case Node.NodeType.EndNode:
                return EndNode != null;
        }

        throw new System.Exception($"不存在{type}类型的节点!");
    }

    private void SetNode(Node node)
    {
        Node temp = null;
        switch (node.Type)
        {
            case Node.NodeType.NormalNode:
                NormalNodes.Add(node.Id, node);
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
                ClearNormalNode(node);
                break;
            case Node.NodeType.StartNode:
                StartNode = null;
                break;
            case Node.NodeType.EndNode:
                EndNode = null;
                break;
        }
    }

    private void ClearNormalNode(Node node)
    {
        if (NormalNodes.ContainsKey(node.Id))
        {
            NormalNodes.Remove(node.Id);
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
