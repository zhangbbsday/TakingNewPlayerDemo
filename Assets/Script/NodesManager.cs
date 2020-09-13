using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesManager
{
    public Transform NodesParent { get; private set; }
    private string NodesParentName { get; } = "Nodes";
    public NodesManager()
    {
        FindParent();
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
