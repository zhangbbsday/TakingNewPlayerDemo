using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButton : MenuButton
{
    enum ButtonType
    {
        Read,
        Write,
    }

    [SerializeField]
    private FunctionMenu menu;
    [SerializeField]
    private ButtonType buttonType;

    protected override void Start()
    {
        base.Start();
        menu.Close();
    }

    public override void PressAction()
    {
        menu.Show();
        InAction();
    }

    public override void ReleseAction()
    {
        menu.Close();
        OutAction();
    }

    private void InAction()
    {
        switch (buttonType)
        {
            case ButtonType.Read:
                ReadFilesIn();
                break;
            case ButtonType.Write:
                WriteFilesIn();
                break;
        }
    }

    private void OutAction()
    {
        switch (buttonType)
        {
            case ButtonType.Read:
                ReadFilesOut();
                break;
            case ButtonType.Write:
                WriteFilesOut();
                break;
        }
    }

    private void ReadFilesIn()
    {
        GameManager.Instance.FileContainerManager.CreateAllFileContainers();
    }

    private void ReadFilesOut()
    {
        GameManager.Instance.FileContainerManager.DeleteAllFileConatiner();
    }

    private void WriteFilesIn()
    {

    }

    private void WriteFilesOut()
    {

    }
}
