using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            var bullet = _combatManager.ProjectileManager.CreateProjectile(bulletPos);
            bullet.Initialize(_direction, actor.Damage, 1000f);

            actor.OnAttack();
        }
    }
}
