using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat.Actions
{
    public abstract class ActionBase
    {
        public abstract void Execute(CharacterUnit actor);
    }
}
