using System;
using UnityEngine;

public class Utils
{
    public static Vector2 BezierCurve(float per, Vector2 a, Vector2 b, Vector2 c)
    {
        Vector2 ab = Vector2.Lerp(a, b, per);
        Vector2 bc = Vector2.Lerp(b, c, per);
        return Vector2.Lerp(ab, bc, per);
    }
}
