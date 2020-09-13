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

    protected override ButtonManager.ButtonEffectType SetButtonEffect()
    {
        return ButtonManager.ButtonEffectType.NormalEffect;
    }
}
