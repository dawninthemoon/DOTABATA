using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Combat
{
    public class PlayerTurret : TurretBase, IProgressiveInteractable
    {
        protected float _interactingTime;
        private static readonly float TargetInteractingTime = 2f;
        protected override float FireCooldown { get => 20f; }
        protected override int MaxShells { get => 1; }

        public override void Initialize()
        {
            base.Initialize();
            _interactingTime = 0f;
        }

        public void InteractProgress(CharacterUnit characterUnit)
        {
            if (_isProcessing)
            {
                return;
            }
            
            _isInteracting = true;
            _interactingTime += Time.deltaTime;

            if (_interactingTime >= TargetInteractingTime)
            {
                StartAttack();
            }
        }

        public void InteractEnd(CharacterUnit characterUnit)
        {
            _isInteracting = false;
            _interactingTime = 0f;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            var target = _combatManager.EnemyManager.EnemyVehicle;
            if (target != null && target.CurrentHP >= 0)
            {
                if (CanAttack() && target.CanTarget())
                {
                    ProcessAttack(target.HitPoint);
                }
            }
        }

        protected override void OnAttack()
        {
            base.OnAttack();

            float radian = GetBulletDegree() * Mathf.Deg2Rad;
            Vector3 dir = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian)).normalized;

            Vector3 firePos = transform.position + (Vector3)dir;
            var projectile = _combatManager.ProjectileManager.CreateProjectile("AllyTurretBullet", firePos);
            projectile.Initialize(dir, 1, 1000);
        }
    }
}