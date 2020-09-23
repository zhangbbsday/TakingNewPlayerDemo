using GameEditor;
using System;
using UnityEngine.SceneManagement;

public class StartGameButton : ButtonBase
{
    protected override ButtonManager.ButtonEffectType SetButtonEffect()
    {
        return ButtonManager.ButtonEffectType.NormalEffect;
    }

    protected override Action AddMethod()
    {
        return () => StartGame();
    }

    private void StartGame()
    {
        SceneUtils.ChangeScene("LevelSelectScene");
    }
}
