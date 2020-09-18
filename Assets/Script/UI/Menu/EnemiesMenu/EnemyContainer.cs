using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EnemyContainer : UIElementBase
{
    public int Id { get; private set; }
    private Text IdText { get; set; }
    private Dropdown EnemyTypeSelecter { get; set; }

    public void Init(int id, string[] options)
    {
        Id = id;

        SetOptions(options);
    }

    public EnemyContainerManager.EnemyType GetEnemyType()
    {
        int value = EnemyTypeSelecter.value;
        return GameManager.Instance.EnemyContainerManager.GetEnemyTypeByValue(value); 
    }

    protected override void Start()
    {
        base.Start();
        CanChangePosition = false;

        IdText = Transform.Find("Id").Find("Number").GetComponent<Text>();
        IdText.text = Id.ToString();
    }

    private void SetOptions(string[] options)
    {
        EnemyTypeSelecter = Transform.Find("ChooseType").GetComponent<Dropdown>();
        EnemyTypeSelecter.ClearOptions();
        EnemyTypeSelecter.AddOptions(options.ToList());
    }
}
