using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class EnemyVehicle : MonoBehaviour
    {
        [field: SerializeField]
        public EnemyVehicleHitPoint HitPoint { get; private set; }
        [SerializeField]
        private EnemyTurret turret;

        private static readonly int MaxHP = 2;

        private int _currentHP;
        public int CurrentHP => _currentHP;

        private void Awake()
        {
            HitPoint.SetDependency(this);
        }
        
        public void Initialize()
        {
            _currentHP = MaxHP;
            turret.Initialize();
        }

        public void SetDependency(CombatManager combatManager)
        {
            turret.SetDependency(combatManager);
        }

        public void ReceiveDamage(int damage, UnitBase attacker)
        {
            _currentHP -= damage;
            if (_currentHP <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _currentHP = 0;
        }

        public bool CanTarget()
        {
            return _currentHP > 0;
        }
    }
}