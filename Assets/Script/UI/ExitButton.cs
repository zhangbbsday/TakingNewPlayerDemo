using System;

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

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
        }
    }
}