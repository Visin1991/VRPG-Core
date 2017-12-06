using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extention {

    public static Vector3 RandomPos(this Vector3 vector3,float from,float to)
    {
        return new Vector3(Random.Range(from, to), Random.Range(from, to), Random.Range(from, to));
    }

    public static Vector3 RandomPosXZ(this Vector3 vector3, float from, float to)
    {
        return new Vector3(Random.Range(from, to), 0, Random.Range(from, to));
    }

    public static Vector3 MultiplyAdd(this Vector3 V3, Vector3 V1, Vector3 V2)
    {
        Vector3 Result;
        Result.x = V1.x * V2.x + V3.x;
        Result.y = V1.y * V2.y + V3.y;
        Result.z = V1.z * V2.z + V3.z;
        return Result;
    }

    public static Vector3 RotateAxis(this Vector3 v, Vector3 axis, float angle)
    {
        return Quaternion.AngleAxis(angle, axis) * v;
    }

    
}
