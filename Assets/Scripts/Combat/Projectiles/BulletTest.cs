using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class BulletTest : MonoBehaviour
    {
        [SerializeField]
        private ColliderTargeter targeter;

        private Vector3 _dir;
        private int _damage;
        private float _moveSpeed;
        private bool _initialized;

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
        }

        private void Update()
        {
            if (!_initialized)
            {
                return;
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