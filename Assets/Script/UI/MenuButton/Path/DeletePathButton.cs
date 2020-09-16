using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePathButton : MenuButton
{
    private bool IsSelecting { get; set; }
    private float SelectRange { get; } = 0.5f;
    private Link SelectedOne { get; set; }


    protected override void Update()
    {
        base.Update();
        DeleteLink();
    }

    private void DeleteLink()
    {
        if (!IsSelecting)
            return;

        Link link = FindNearestLik();
        SelectEffect(link);
        if (link == null)
            return;

        if (Input.GetMouseButtonDown(0) && !MouseUtils.IsMouseOverUIObject())
            DeleteOne(link);
    }


    private Link FindNearestLik()
    {
        Link[] links = GameManager.Instance.LinksManager.GetLinks();
        if (links == null || links.Length == 0)
            return null;

        KeyValuePair<Link, float> nearest = new KeyValuePair<Link, float>(links[0], float.MaxValue);
        foreach (var n in links)
        {
            float distance = Vector2.Distance(n.Position, MouseUtils.MouseWorldPosition);
            if (distance < nearest.Value)
                nearest = new KeyValuePair<Link, float>(n, distance);
        }

        if (nearest.Value < SelectRange)
            return nearest.Key;
        return null;
    }

    private void SelectEffect(Link link)
    {
        if (SelectedOne == null)
            SelectedOne = link;
        else
        {
            SelectedOne.Transform.localScale = Vector3.one;
            SelectedOne = link;
        }

        if (SelectedOne != null)
            SelectedOne.Transform.localScale = new Vector3(1, 1, 0) * 1.2f;
    }

    private void DeleteOne(Link link)
    {
        GameManager.Instance.LinksManager.DeleteLink(link);
    }

    public override void PressAction()
    {
        IsSelecting = true;

        SelectedOne = null;
    }

    public override void ReleseAction()
    {
        IsSelecting = false;
        SelectedOne = null;
    }
}
