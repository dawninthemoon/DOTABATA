using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class EnemyTurret : TurretBase
    {
        protected override float FireCooldown { get => 2f; }
        protected override int MaxShells { get => 9999; }

        //protected override float FireCooldown { get => 10f; }

        public override void Initialize()
        {
            base.Initialize();
            StartAttack();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            var target = _combatManager.VehicleManager.Core;

            if (target != null && target.CurrentHP >= 0)
            {
                if (CanAttack() && target.CanTarget())
                {
                    ProcessAttack(target);
                }
            }
        }

        protected override void OnAttack()
        {
            base.OnAttack();

            float radian = GetBulletDegree() * Mathf.Deg2Rad;
            Vector3 dir = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian)).normalized;

            Vector3 firePos = transform.position + (Vector3)dir;
            var projectile = _combatManager.ProjectileManager.CreateProjectile("EnemyTurretBullet", firePos);
            projectile.Initialize(dir, 1, 1000);
        }
    }
}