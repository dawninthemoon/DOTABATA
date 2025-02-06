using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Combat
{
    public class PlayerTurret : TurretBase, IProgressiveInteractable
    {
        private bool _isInteracting;
        private float _interactingTime;
        private bool _isProcessing;
        private int _remainShells;
        private float _attackWaitTimer;

        private static readonly float TargetInteractingTime = 2f;
        //private static readonly float FireCooldown = 20f;
        private static readonly float FireCooldown = 2f;

        private CombatManager _combatManager;

        [SerializeField]
        private float angleRate;

        public void Initialize()
        {
            _isInteracting = false;
            _interactingTime = 0f;
            _isProcessing = false;
            _attackWaitTimer = 0f;
        }

        public void SetDependency(CombatManager combatManager)
        {
            _combatManager = combatManager;
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

        public void StartAttack()
        {
            _isProcessing = true;
            _remainShells = 1;
        }

        public bool CanAttack()
        {
            return _remainShells > 0;
        }
        
        private void Update()
        {
            if (!_isProcessing)
            {
                return;
            }

            var target = _combatManager.EnemyManager.EnemyVehicle;
            if (target != null && target.CurrentHP >= 0)
            {
                if (CanAttack() && target.CanTarget())
                {
                    ProcessAttack(target);
                }
            }
        }

        private void ProcessAttack(EnemyVehicle target)
        {
            Vector2 diff = (target.HitPoint.GetPosition() - (Vector2)transform.position).normalized;
            float myRadian = GetBulletDegree() * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(myRadian), Mathf.Sin(myRadian)).normalized;

            Debug.DrawRay(transform.position, dir * 100f, Color.green);

            float dot = Vector2.Dot(diff, dir);
            if (Mathf.Abs(1f - dot) > 0.0001f)
            {
                float rotateAmount = -angleRate * Time.deltaTime;
                transform.Rotate(0f, 0f, rotateAmount);
            }
            else
            {
                ProcessFire();
            }
        }

        private void ProcessFire()
        {
            if (_attackWaitTimer > 0f)
            {
                _attackWaitTimer -= Time.deltaTime;
            }

            if (_attackWaitTimer <= 0f)
            {
                Fire();
            }
        }

        private void Fire()
        {
            _attackWaitTimer = FireCooldown;

            float radian = GetBulletDegree() * Mathf.Deg2Rad;
            Vector3 dir = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian)).normalized;

            Vector3 firePos = transform.position + (Vector3)dir;
            var projectile = _combatManager.ProjectileManager.CreateProjectile("AllyTurretBullet", firePos);
            projectile.Initialize(dir, 1, 1000);

            _remainShells -= 1;
        }

        private float GetBulletDegree()
        {
            return transform.localRotation.eulerAngles.z + 90f;
        }
    }
}