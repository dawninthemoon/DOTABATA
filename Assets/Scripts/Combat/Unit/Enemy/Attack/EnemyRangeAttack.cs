using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class EnemyRangeAttack : IEnemyAttack
    {
        public void RequestAttack(EnemyUnit attacker, CombatManager combatManager)
        {
            Vector2 dir = (attacker.SelectedTarget.GetPosition() - attacker.GetPosition()).normalized;
            Vector2 bulletPos = attacker.GetPosition() + dir * 40f;
            var bullet = combatManager.ProjectileManager.CreateProjectile("EnemyBullet", bulletPos);

            bullet.Initialize(dir, attacker.Damage, 200f);
        }
    }
}
