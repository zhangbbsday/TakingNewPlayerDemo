using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButton : MenuButton
{
    [SerializeField]
    private FunctionMenu menu;

    protected override void Start()
    {
        base.Start();
        menu.Close();
    }

    public override void PressAction()
    {
        menu.Show();
    }

    public override void ReleseAction()
    {
        menu.Close();
    }
}
