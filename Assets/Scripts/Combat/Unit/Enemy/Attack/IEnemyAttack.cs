using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public interface IEnemyAttack
    {
        public void RequestAttack(EnemyUnit attacker, CombatManager combatManager);
    }
}