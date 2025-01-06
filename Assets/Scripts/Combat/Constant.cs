using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Combat
{
    public static class Constant
    {
        public static readonly Vector2[] EightDirections = new Vector2[]
        {
            new Vector2(0f, 1f).normalized,
            new Vector2(1f, 1f).normalized,
            new Vector2(1f, 0f).normalized,
            new Vector2(1f, -1f).normalized,
            new Vector2(0f, -1f).normalized,
            new Vector2(-1f, -1f).normalized,
            new Vector2(-1f, 0f).normalized,
            new Vector2(-1f, 1f).normalized,
        };
    }
}