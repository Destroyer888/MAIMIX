using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathHelper
{
    public static float CalculateAngleFromVector(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x)*57;
        return angle;
    }
}
