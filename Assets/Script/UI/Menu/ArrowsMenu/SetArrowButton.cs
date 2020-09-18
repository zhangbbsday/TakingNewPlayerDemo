using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetArrowButton : MenuButton
{
    [SerializeField]
    private Arrow.ArrowType buttonType;
    private bool IsPlacing { get; set; }
    private Arrow PlacedOne { get; set; }

    protected override void Update()
    {
        base.Update();
        SetArrow();
    }

    private void SetArrow()
    {
        if (!IsPlacing)
            return;

        if (PlacedOne != null)
            ChangeDirection();

        if (Input.GetMouseButtonDown(0) && !MouseUtils.IsMouseOverUIObject())
        {
            if (PlacedOne != null)
                SetDirection();
            else
                PlaceOne();
        }
    }

    private void PlaceOne()
    {
        Vector2 pos = MouseUtils.MouseWorldPosition;
        PlacedOne = GameManager.Instance.ArrowsManager.CreateArrow(buttonType, pos);
    }

    private void ChangeDirection()
    {
        Vector2 dir = MouseUtils.MouseWorldPosition - PlacedOne.Position;
        PlacedOne.SetDirection(dir);
    }

    private void SetDirection()
    {
        Vector2 dir = MouseUtils.MouseWorldPosition - PlacedOne.Position;
        PlacedOne.SetDirection(dir);

        ReleseAction();
        ButtonEffect.CancelEffect();
    }

    public override void PressAction()
    {
        IsPlacing = true;
        PlacedOne = null;
    }

    public override void ReleseAction()
    {
        IsPlacing = false;
        PlacedOne = null;
    }
}
