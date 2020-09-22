using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEditor
{
    public static class VectorUtils
    {
        public static float DistanceFromPoint2Line(Vector3 p, Vector3 p1, Vector3 p2)
        {
            float distance = Vector3.Distance(p2, p);
            float dotResult = Vector3.Dot(p2 - p1, p2 - p);
            float seitaRad = Mathf.Acos(dotResult / ((p2 - p1).magnitude * distance));
            return distance * Mathf.Sin(seitaRad);
        }

        public static float DistanceFromPoint2Line(Vector2 p, Vector2 p1, Vector2 p2)
        {
            float distance = Vector2.Distance(p2, p);
            float dotResult = Vector2.Dot(p2 - p1, p2 - p);
            float seitaRad = Mathf.Acos(dotResult / ((p2 - p1).magnitude * distance));
            return distance * Mathf.Sin(seitaRad);
        }
    }
}