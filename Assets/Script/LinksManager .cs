using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinksManager
{
    public Transform LinksParent { get; private set; }
    private string LinksParentName { get; } = "Links";
    private Dictionary<int, Link> Links { get; } = new Dictionary<int, Link>();
    private string LinksName { get; } = "Link";
    private int GlobalIndex { get => globalIndex++; }
    private int globalIndex;
    public LinksManager()
    {
        FindParent();
        globalIndex = 0;

    }

    public Link CreateLink(Node left, Node right)
    {
        Link link = GameManager.Instance.ResourcesManager.GetLink(LinksName);
        link = GameObject.Instantiate(link, Vector2.zero, Quaternion.identity, GameManager.Instance.NodesManager.NodesParent);
        link.Init(GlobalIndex, left, right);

        SetLink(link);
        return link;
    }

    public void DeleteLink(Link link)
    {
        ClearLink(link);
        GameObject.Destroy(link.GameObject);
    }

    private void SetLink(Link link)
    {
        Links.Add(link.Id, link);
    }

    private void ClearLink(Link link)
    {
        if (Links.ContainsKey(link.Id))
            Links.Remove(link.Id);
    }

    private void FindParent()
    {
        GameObject obj = GameObject.Find(LinksParentName);
        if (obj == null)
            obj = new GameObject(LinksParentName);

        LinksParent = obj.transform;
        LinksParent.transform.position = Vector2.zero;
    }
}
