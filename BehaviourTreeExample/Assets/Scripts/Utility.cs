using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static float Remap(float input, float oldMin, float oldMax, float newMin, float newMax)
    {
        return (input - oldMin) / (oldMax - oldMin) * (newMax - newMin) + newMin;
    }
}
