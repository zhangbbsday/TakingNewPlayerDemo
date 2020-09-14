using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager
{
    private Dictionary<string, Node> Nodes { get; } = new Dictionary<string, Node>();
    private Dictionary<string, Link> Links { get; } = new Dictionary<string, Link>();

    private string[] NodesPath { get; } =
    {
        "Prefabs/Nodes/StartNode",
        "Prefabs/Nodes/EndNode",
        "Prefabs/Nodes/Node",
    };

    private string[] LinkPath { get; } =
    {
        "Prefabs/Links/Link",
    };

    public ResourcesManager()
    {
        Load();
    }

    private void Load()
    {
        LoadNodes();
        LoadLinks();
    }

    public Node GetNode(string name)
    {
        return Nodes.ContainsKey(name) ? Nodes[name] : null;
    }

    public Link GetLink(string name)
    {
        return Links.ContainsKey(name) ? Links[name] : null;
    }

    private void LoadNodes()
    {
        foreach (var path in NodesPath)
        {
            Node obj = Resources.Load<Node>(path);

            if (Nodes.ContainsKey(obj.name))
                throw new System.Exception($"存在重名文件{obj.name}!");

            Nodes.Add(obj.name, obj);
        }
    }

    private void LoadLinks()
    {
        foreach (var path in LinkPath)
        {
            Link obj = Resources.Load<Link>(path);

            if (Links.ContainsKey(obj.name))
                throw new System.Exception($"存在重名文件{obj.name}!");

            Links.Add(obj.name, obj);
        }
    }
}
