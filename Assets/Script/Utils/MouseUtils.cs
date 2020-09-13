using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MouseUtils
{
    public static Vector2 MouseScreenPosition { get => Input.mousePosition; }
    public static Vector2 MouseWorldPosition { get => Camera.main.ScreenToWorldPoint(MouseScreenPosition); }
}
