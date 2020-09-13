using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuButton : ButtonBase
{
    protected override void Start()
    {
        base.Start();
        CanChangePosition = false;
    }

    protected override Action AddMethod()
    {
        return () => ButtonAction();
    }

    protected abstract void PressAction();
    protected abstract void ReleseAction();

    protected override ButtonManager.ButtonEffectType SetButtonEffect()
    {
        return ButtonManager.ButtonEffectType.NormalEffect;
    }

    private void ButtonAction()
    {
        if (ButtonManager.Instance.MenuButtonActiveNow != null)
            ButtonManager.Instance.MenuButtonActiveNow.ReleseAction();

        PressAction();
        ButtonManager.Instance.MenuButtonActiveNow = this;
    }
}
