using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionButton : ButtonBase
{
    protected override ButtonManager.ButtonEffectType SetButtonEffect()
    {
        return ButtonManager.ButtonEffectType.NormalEffect;
    }
}
