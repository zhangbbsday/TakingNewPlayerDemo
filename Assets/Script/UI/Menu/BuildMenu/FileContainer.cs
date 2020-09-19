using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileContainer : UIElementBase
{
    public int Id { get; private set; }
    public string FileName { get; private set; }
    public string FileUpdateDate { get; private set; }

    private Text FileNameText { get; set; }
    private Text FileUpdateDateText { get; set; }

    public void Init(int id, string fileName, string updateDate)
    {
        Id = id;
        FileName = fileName;
        FileUpdateDate = updateDate;
    }

    protected override void Start()
    {
        base.Start();
        CanChangePosition = false;

        SetText();
    }

    private void SetText() 
    {
        FileNameText = Transform.Find("FileName").GetComponent<Text>();
        FileUpdateDateText = Transform.Find("UpdateDate").GetComponent<Text>();

        FileNameText.text = FileName;
        FileUpdateDateText.text = FileUpdateDate;
    }
}
