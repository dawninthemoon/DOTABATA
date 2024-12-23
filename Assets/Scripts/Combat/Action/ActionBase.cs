using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat.Actions
{
    public abstract class ActionBase
    {
        protected CombatManager _combatManager;

        public void SetDependency(CombatManager combatManager)
        {
            _combatManager = combatManager;
        }

        public abstract void Execute(CharacterUnit actor);
    }
}
