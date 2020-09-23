using GameEditor;
using System;
using UnityEngine;

public class ExitButton : ButtonBase
{
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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
}
