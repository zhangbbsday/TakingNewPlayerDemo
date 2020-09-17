using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyContainer : UIElementBase
{
    public int Id { get; private set; }
    private Text IdText { get; set; }

    public void Init(int id)
    {
        Id = id;
    }

    protected override void Start()
    {
        base.Start();
        CanChangePosition = false;

        IdText = Transform.Find("Id").Find("Number").GetComponent<Text>();
        IdText.text = Id.ToString();
    }
}
