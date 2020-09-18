using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : GameActor, ISelectableActor
{
    public enum ArrowType
    {
        AttackArrow,
        ReturnArrow,
    }

    public int Id { get; private set; }
    public Vector2 Position { get; set; }
    public Vector2 Direction { get; private set; }
    public ArrowType Type { get; private set; }
    private float SelectedSize { get; } = 0.2f;
    private Vector2 StartScale { get; set; }

    public void Init(int id, Vector2 pos, Vector2 direction, ArrowType type)
    {
        Id = id;
        Position = pos;
        Direction = direction;
        Type = type;
    }

    protected override void Start()
    {
        base.Start();
        Position = Transform.position;
        StartScale = Transform.localScale;
        SetDirection(Direction);
    }

    protected override void Update()
    {
        base.Update();

        Transform.position = Position;
    }

    public void SelectEffect()
    {
        Transform.localScale += new Vector3(1, 1, 0) * SelectedSize;
    }

    public void ReleaseEffect()
    {
        Transform.localScale = StartScale;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
        float angle = Vector2.SignedAngle(Vector2.up, Direction);
        Transform.eulerAngles = new Vector3(Transform.eulerAngles.x, Transform.eulerAngles.y, angle);
    }
}
