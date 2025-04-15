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
            _status.mouse0 = Input.GetMouseButton(0);
            _status.interaction = Input.GetKeyDown(KeyCode.E);
            _status.interactionHolding = Input.GetKey(KeyCode.E);
            _status.interactionEnd = Input.GetKeyUp(KeyCode.E);
            _status.reload = Input.GetKeyDown(KeyCode.R);
            _status.activeSkill = Input.GetKeyDown(KeyCode.Space);
        }
    }
}
