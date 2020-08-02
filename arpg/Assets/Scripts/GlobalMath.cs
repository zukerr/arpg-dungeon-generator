using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMath
{
    public static float SquareFunctionPositiveA(float x, float root1, float root2, float scale)
    {
        return (x - root1) * (x - root2) * (scale / 1.5f);
    }
    public static float SquareFunctionNegativeA(float x, float root1, float root2, float scale)
    {
        return (-x + root1) * (x - root2) * (scale / 1.5f);
    }
}
