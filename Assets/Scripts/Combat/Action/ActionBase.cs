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

        public virtual bool CanMove()
        {
            return true;
        }

        public virtual bool CanAttack()
        {
            return true;
        }

        public virtual bool CanInteract()
        {
            return true;
        }
    }
}
