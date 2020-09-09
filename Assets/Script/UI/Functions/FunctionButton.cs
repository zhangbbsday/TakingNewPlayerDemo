using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FunctionButton : ButtonBase
{
    [SerializeField]
    private FunctionMenu menu;

    protected override void Start()
    {
        base.Start();   
        CanChangePosition = false;
    }

    protected override ButtonManager.ButtonEffectType SetButtonEffect()
    {
        return ButtonManager.ButtonEffectType.NormalEffect;
    }

    protected override Action AddMethod()
    {
        return () => ShowMenu();
    }

    private void ShowMenu()
    {

    }
}
