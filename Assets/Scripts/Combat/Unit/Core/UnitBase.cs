using System.Collections;
using System.Collections.Generic;
using Game.Utils;
using UnityEngine;

namespace Combat
{
    public class UnitBase : MonoBehaviour, ITargetable, IEffectAttachable
    {
        [SerializeField]
        private Transform _effectRoot;
        [SerializeField]
        protected float moveSpeed;
        [SerializeField]
        private float attackSpeed;

        public Transform EffectRoot => _effectRoot;
        private float _attackWaitTimer;
        protected StageManager _stageManager;

        public void SetDependency(StageManager stageManager)
        {
            _stageManager = stageManager;
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
            _attackWaitTimer = 1f / attackSpeed;
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