using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class EnemyRangeAttack : IEnemyAttack
    {
        public void RequestAttack(EnemyUnit attacker, CombatManager combatManager)
        {
            Vector2 bulletPos = attacker.GetPosition() + attacker.Direction * 40f;
            var bullet = combatManager.ProjectileManager.CreateProjectile("EnemyBullet", bulletPos);

            bullet.Initialize(attacker.Direction, attacker.Damage, 200f);

        }
    }
}
