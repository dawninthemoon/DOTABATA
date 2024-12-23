using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat.Action
{
    public abstract class ActionBase
    {
        public abstract void Use(CharacterUnit actor);
    }
}
