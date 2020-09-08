using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIElementBase : GameActor
{
    public Vector2 Position { get; set; }

    protected override void Start()
    {
        base.Start();
        Pretreatment();
    }

    protected override void Update()
    {
        base.Update();
        UpdateBase();
    }

    private void Pretreatment()
    {
        Position = Transform.localPosition;
    }

    private void UpdateBase()
    {
        Transform.localPosition = Position;
    }
}
