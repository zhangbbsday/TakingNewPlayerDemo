using System;
using UnityEngine;

namespace GameEditor
{
    public class ExitButton : ButtonBase
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
        protected override Action AddMethod()
        {
            return () => Exit();
        }
        private void Exit()
        {
            if (ButtonManager.Instance.FunctionButtonActiveNow != null)
            {
                ButtonManager.Instance.FunctionButtonActiveNow.CloseMenu();
                ButtonManager.Instance.FunctionButtonActiveNow = null;
                return;
            }

            SceneUtils.ChangeScene("Start");
        }
    }
}