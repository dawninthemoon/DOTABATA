using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Utils;

namespace Combat.Actions
{
    public class FireAction : ActionBase
    {
        private Vector2 _direction;

        public void SetDirection(Vector2 dir)
        {
            _direction = dir;
        }

        public override void Execute(CharacterUnit actor)
        {
            Vector3 bulletPos = actor.BulletTransform.position;
            var bullet = _combatManager.ProjectileManager.CreateProjectile("YuriBullet", bulletPos);

            float spreadAmount = Random.Range(-actor.Weapon.SpreadDegree, actor.Weapon.SpreadDegree);
            bullet.Initialize(_direction.AddDegree(spreadAmount), actor.Damage, actor.Weapon.BulletSpeed);

            actor.OnAttack();
        }
    }
}
