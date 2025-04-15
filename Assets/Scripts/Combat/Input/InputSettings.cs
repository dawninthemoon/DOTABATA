using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class InputStatus
    {
        public Vector2 direction;
        public bool mouse0;
        public bool interaction;
        public bool interactionHolding;
        public bool interactionEnd;
        public bool reload;
        public bool activeSkill;

        public void Reset()
        {
            direction = Vector2.zero;
            mouse0 = false;
            interaction = false;
            interactionHolding = false;
            interactionEnd = false;
            reload = false;
        }
    }
}