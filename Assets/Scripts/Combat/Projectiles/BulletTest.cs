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

        private void Awake()
        {
            _initialized = false;
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
            var targetUnit = targeter.CurrentTarget as UnitBase;
            if (targetUnit != null)
            {
                targetUnit.ReceiveDamage(_damage);
                Release();
            }
        }

        private void Release()
        {
            _initialized = false;
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}