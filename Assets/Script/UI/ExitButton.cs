﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
}