using System.Collections;
using System.Collections.Generic;
using Game.Utils;
using UnityEngine;

namespace Combat
{
    public class BulletTest : MonoBehaviour
    {
        [SerializeField]
        private ColliderTargeter targeter;
        [SerializeField]
        private float lifeTime;

        private Vector3 _dir;
        private int _damage;
        private float _moveSpeed;
        private bool _initialized;

        private float _remainTime;

        private CombatManager _combatManager;

        private void Awake()
        {
            _initialized = false;
        }

        public void SetDependency(CombatManager combatManager)
        {
            _combatManager = combatManager;
        }

        public void Initialize(Vector3 dir, int damage, float moveSpeed)
        {
            gameObject.SetActive(true);
            
            _dir = dir;
            _damage = damage;
            _moveSpeed = moveSpeed;
            _initialized = true;

            _remainTime = lifeTime;
        }

        private void Update()
        {
            if (!_initialized)
            {
                return;
            }

            _remainTime -= Time.deltaTime;
            if (_remainTime <= 0f)
            {
                Release();
            }

            transform.position += _dir * _moveSpeed * Time.deltaTime;
            var targetUnit = targeter.CurrentTarget;
            if (targetUnit != null)
            {
                targetUnit.ReceiveDamage(_damage);
                Release();
            }
        }

        private void Release()
        {
            _initialized = false;

            _combatManager.EffectManager.CreateEffect("BulletDestroy", transform.position);

            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}