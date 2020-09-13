using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class MouseUtils
{
    public static Vector2 MouseScreenPosition { get => Input.mousePosition; }
    public static Vector2 MouseWorldPosition { get => Camera.main.ScreenToWorldPoint(MouseScreenPosition); }

    public static bool IsMouseOverUIObject()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
