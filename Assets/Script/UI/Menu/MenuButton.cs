using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEditor
{
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

        protected void OnDisable()
        {
            ReleseAction();

            if (ButtonManager.Instance.MenuButtonActiveNow != null)
                ButtonManager.Instance.MenuButtonActiveNow.ButtonEffect.CancelEffect();
        }

        public abstract void PressAction();
        public abstract void ReleseAction();

        protected override ButtonManager.ButtonEffectType SetButtonEffect()
        {
            return ButtonManager.ButtonEffectType.NormalEffect;
        }

        private void ButtonAction()
        {
            if (ButtonManager.Instance.MenuButtonActiveNow != null && ButtonManager.Instance.MenuButtonActiveNow.GameObject.activeSelf)
            {
                ButtonManager.Instance.MenuButtonActiveNow.ReleseAction();
                ButtonManager.Instance.MenuButtonActiveNow.ButtonEffect.CancelEffect();
            }

            PressAction();
            ButtonManager.Instance.MenuButtonActiveNow = this;
            ButtonEffect.SelectedEffect();
        }
    }
}