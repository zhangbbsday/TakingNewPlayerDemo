using GameEditor;
using System;
using UnityEngine.SceneManagement;

public class GameEditorButton : ButtonBase
{
    protected override ButtonManager.ButtonEffectType SetButtonEffect()
    {
        return ButtonManager.ButtonEffectType.NormalEffect;
    }

    protected override Action AddMethod()
    {
        return () => GameEditor();
    }

    private void GameEditor()
    {
        SceneUtils.ChangeScene("GameEditor");
    }
}
