using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public abstract class TurretBase : MonoBehaviour
    {
        protected bool _isInteracting;
        protected bool _isProcessing;
        protected int _remainShells;
        protected float _attackWaitTimer;
        private float _initialDegree;

        //private static readonly float FireCooldown = 20f;
        protected abstract float FireCooldown { get; }
        protected abstract int MaxShells { get; }

        protected CombatManager _combatManager;

        [SerializeField]
        private float angleRate;

        private void Awake()
        {
            _initialDegree = transform.localRotation.eulerAngles.z;
        }

        public virtual void Initialize()
        {
            _isInteracting = false;
            _isProcessing = false;
            _attackWaitTimer = 0f;
        }

        public void SetDependency(CombatManager combatManager)
        {
            _combatManager = combatManager;
        }

        public void StartAttack()
        {
            _isProcessing = true;
            _remainShells = MaxShells;
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

            OnUpdate();
        }

        protected virtual void OnUpdate()
        {

        }

        protected virtual void ProcessAttack(ITargetable target)
        {
            Vector2 diff = (target.GetPosition() - (Vector2)transform.position).normalized;
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
            _remainShells -= 1;

            OnAttack();
        }

        protected virtual void OnAttack()
        {
            
        }

        protected float GetBulletDegree()
        {
            return transform.localRotation.eulerAngles.z + 90f;
        }
    }
}