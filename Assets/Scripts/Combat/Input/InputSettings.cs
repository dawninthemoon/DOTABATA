using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class InputStatus
    {
        public Vector2 direction;
        public bool mouseDown;

        public void Reset()
        {
            direction = Vector2.zero;
            mouseDown = false;
        }
    }
}