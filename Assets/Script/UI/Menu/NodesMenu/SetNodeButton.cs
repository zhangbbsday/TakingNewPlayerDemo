using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SetNodeButton : MenuButton
{
    [SerializeField]
    private Node.NodeType buttonType;
    private bool IsPlacingNode { get; set; } = false;
    protected override void Update()
    {
        base.Update();
        PlaceNode();
    }

    private void PlaceNode()
    {
        if (!IsPlacingNode)
            return;

        if (Input.GetMouseButtonDown(0) && !MouseUtils.IsMouseOverUIObject())
            PlaceOne();
    }

    private void PlaceOne()
    {
        Vector2 pos = MouseUtils.MouseWorldPosition;
        GameManager.Instance.NodesManager.CreateNode(buttonType, pos);
        //IsPlacingNode = false;
    }

    public override void PressAction()
    {
        IsPlacingNode = true;
    }

    public override void ReleseAction()
    {
        IsPlacingNode = false;
    }
}
