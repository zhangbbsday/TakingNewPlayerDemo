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

    private Dictionary<ButtonEffectType, IButtonEffect> Effects { get; } = new Dictionary<ButtonEffectType, IButtonEffect>
    {
        [ButtonEffectType.None] = null,
        [ButtonEffectType.NormalEffect] = new NormalButtonEffect(),
    };
    private ButtonManager()
    {

    }

    public IButtonEffect GetEffect(ButtonEffectType effect)
    {
        if (Effects.ContainsKey(effect))
            return Effects[effect];
        return null;
    }
    public void SetActiveFunctionButton(FunctionButton button)
    {
        FunctionButtonActiveNow = button;
    }
}
