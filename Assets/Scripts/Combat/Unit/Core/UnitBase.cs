using System.Collections;
using System.Collections.Generic;
using Game.Utils;
using UnityEngine;

namespace Combat
{
    public abstract class UnitBase : MonoBehaviour, ITargetable, IEffectAttachable
    {
        [SerializeField]
        private Transform _effectRoot;
        public virtual TargetFaction Faction => TargetFaction.None;

        protected int _health;

        public virtual int MaxHP { get; }
        public int CurrentHP => _health;
        public virtual int AttackPower { get; }
        public virtual float AttackSpeed { get; }
        public virtual int Damage { get; }

        public Transform EffectRoot => (_effectRoot != null) ? _effectRoot : transform;
        private float _attackWaitTimer;
        protected CombatManager _combatManager;
        public event System.Action<ITargetable> OnDisappear;

        protected virtual void Start()
        {
            
        }

        public void SetDependency(CombatManager combatManager)
        {
            _combatManager = combatManager;
        }

        public virtual void Progress()
        {
            
        }

        public Vector2 GetPosition()
        {
            return transform.position;
        }

        public void SetPosition(Vector2 position)
        {
            transform.position = new Vector3(position.x, position.y, transform.position.z);
        }

        public void AddPosition(Vector2 position)
        {
            SetPosition(GetPosition() + position);
        }

        public void Release()
        {
            gameObject.SetActive(false);
        }

        public virtual void ReceiveDamage(int damage, UnitBase attacker = null)
        {
            _health -= damage;
            if (IsDie())
            {
                OnDie(attacker);
            }
        }

        protected virtual bool IsDie()
        {
            return _health <= 0;
        }

        protected virtual void OnDie(UnitBase attacker)
        {
            
        }

        public virtual bool CanAttack()
        {
            return _attackWaitTimer <= 0f;
        }

        public virtual void OnAttack()
        {
            _attackWaitTimer = 1f / AttackSpeed;
        }

        public virtual bool CanTarget()
        {
            return !IsDie();
        }

        protected virtual void ProcessAttackWaitTimer()
        {
            if (_attackWaitTimer > 0f)
            {
                _attackWaitTimer -= Time.deltaTime;
            }
        }
    }
}