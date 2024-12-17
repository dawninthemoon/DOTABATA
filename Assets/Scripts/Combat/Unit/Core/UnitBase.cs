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
        protected virtual float _attackSpeed => 1f;
        public virtual TargetFaction Faction => TargetFaction.None;

        public Transform EffectRoot => (_effectRoot != null) ? _effectRoot : transform;
        private float _attackWaitTimer;
        protected StageManager _stageManager;
        public event System.Action<UnitBase> OnDisappear;

        public void SetDependency(StageManager stageManager)
        {
            _stageManager = stageManager;
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

        public bool CanAttack()
        {
            return _attackWaitTimer <= 0f;
        }

        protected virtual void OnAttack()
        {
            _attackWaitTimer = 1f / _attackSpeed;
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