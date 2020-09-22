using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEditor
{
    public class ButtonManager
    {
        public enum ButtonEffectType
        {
            None,
            NormalEffect,
            NoTextEffect,
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
        public FunctionButton FunctionButtonActiveNow { get; set; }
        public MenuButton MenuButtonActiveNow { get; set; }
        private Dictionary<ButtonEffectType, Type> Effects { get; } = new Dictionary<ButtonEffectType, Type>
        {
            [ButtonEffectType.None] = null,
            [ButtonEffectType.NormalEffect] = typeof(NormalButtonEffect),
            [ButtonEffectType.NoTextEffect] = typeof(NoTextButtonEffect),
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
    }
}