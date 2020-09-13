using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SetNodeButton : MenuButton
{
    enum ButtonType
    {
        NormalNode,
        StartNode,
        EndNode,
    }

    private Dictionary<ButtonType, Node.NodeType> ChangeMap = new Dictionary<ButtonType, Node.NodeType>
    {
        [ButtonType.NormalNode] = Node.NodeType.NormalNode,    
        [ButtonType.StartNode] = Node.NodeType.StartNode,    
        [ButtonType.EndNode] = Node.NodeType.EndNode,
    };

    [SerializeField]
    private ButtonType buttonType;
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
        {
            Vector2 pos = MouseUtils.MouseWorldPosition;
            NodeFactory.CreateNode(ChangeMap[buttonType], pos);
            IsPlacingNode = false;
        }
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
