using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Extention{

    public static Vector2 Rotate(this Vector2 vector2, float angle)
    {
        float x = vector2.x;
        float y = vector2.y;
        float newX = x * Mathf.Cos(Mathf.Deg2Rad * angle) - y * Mathf.Sin(Mathf.Deg2Rad * angle);
        float newY = x * Mathf.Sin(Mathf.Deg2Rad * angle) + y * Mathf.Cos(Mathf.Deg2Rad * angle);
        return new Vector2(newX, newY);
    }
}
