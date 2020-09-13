using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NodeFactory
{
    private static string[] NodesName { get; } =
    {
        "Node",
        "StartNode",
        "EndNode",
    };


    public static Node CreateNode(Node.NodeType type, Vector2 position)
    {
        string nodeName = NodesName[(int)type];
        Node node = GameManager.Instance.ResourcesManager.GetNode(nodeName);
        GameObject.Instantiate(node, position, Quaternion.identity, GameManager.Instance.NodesManager.NodesParent);

        return node;
    }
}
