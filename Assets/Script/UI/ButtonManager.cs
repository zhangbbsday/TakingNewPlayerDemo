using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager
{
    public enum ButtonEffectType
    {
        None,
        NormalEffect,
    }

    public static ButtonManager Instance
    {
        get
        {
            if (instacne == null)
                instacne = new ButtonManager();
            return instacne;
        }
    }
    private static ButtonManager instacne;
    public FunctionButton FunctionButtonActiveNow { get; private set; }

    private Dictionary<ButtonEffectType, Type> Effects { get; } = new Dictionary<ButtonEffectType, Type>
    {
        [ButtonEffectType.None] = null,
        [ButtonEffectType.NormalEffect] = typeof(NormalButtonEffect),
    };
    private ButtonManager()
    {

    }

    public IButtonEffect GetEffect(ButtonEffectType effect, ButtonBase button)
    {
        if (Effects.ContainsKey(effect))
            return (IButtonEffect)System.Activator.CreateInstance(Effects[effect], new object[] { button });
        return null;
    }
    public void SetActiveFunctionButton(FunctionButton button)
    {
        FunctionButtonActiveNow = button;
    }
}
