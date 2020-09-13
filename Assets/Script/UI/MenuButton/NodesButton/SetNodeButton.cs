using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNodeButton : MenuButton
{
    protected override Action AddMethod()
    {
        return () => SetNode();
    }

    private void SetNode()
    {
        NodeFactory.CreateNode(Node.NodeType.NormalNode, Vector2.zero);
    }
}
