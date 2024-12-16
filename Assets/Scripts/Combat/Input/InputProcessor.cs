using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class InputProcessor
    {
        private static readonly string HorizontalAxis = "Horizontal";
        private static readonly string VerticalAxis = "Vertical";

        private InputStatus _status;
        public InputStatus Status => _status;
        
        public InputProcessor()
        {
            _status = new();
        }

        public void ProcessInput()
        {
            _status.Reset();

            Vector2 input;
            input.x = Input.GetAxisRaw(HorizontalAxis);
            input.y = Input.GetAxisRaw(VerticalAxis);

            _status.direction = input;
            _status.mouseDown = Input.GetMouseButtonDown(0);
        }
    }
}
