using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager
{
    private Dictionary<string, Node> Nodes { get; } = new Dictionary<string, Node>();

    private string[] NodesPath { get; } =
    {
        "Prefabs/Nodes/StartNode",
        "Prefabs/Nodes/EndNode",
        "Prefabs/Nodes/Node",
    };

    public ResourcesManager()
    {
        Load();
    }

    private void Load()
    {
        LoadNodes();
    }

    public Node GetNode(string name)
    {
        return Nodes.ContainsKey(name) ? Nodes[name] : null;
    }

    private void LoadNodes()
    {
        foreach (var path in NodesPath)
        {
            Debug.Log(path);
            Node node = Resources.Load<Node>(path);

            if (Nodes.ContainsKey(node.name))
                throw new System.Exception($"存在重名文件{node.name}!");

            Nodes.Add(node.name, node);
        }
    }
}
