using System;
using UnityEngine;

public class LoadFileButton : ButtonBase
{
    protected override Action AddMethod()
    {
        return () => PressAction();
    }

    protected override ButtonManager.ButtonEffectType SetButtonEffect()
    {
        return ButtonManager.ButtonEffectType.NormalEffect;
    }

    public void PressAction()
    {
        var container = Transform.parent.GetComponent<FileContainer>();
        GameManager.Instance.BuildManager.LoadFile(container.FileName);

        Debug.Log("文件读取成功!");
    }
}
