using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class EnemyVehicleHitPoint : MonoBehaviour, ITargetable
    {
        public TargetFaction Faction { get => TargetFaction.EnemyVehicle; }
        public event System.Action<ITargetable> OnDisappear;

        private EnemyVehicle _owner;

        public void SetDependency(EnemyVehicle owner)
        {
            _owner = owner;
        }

        public Vector2 GetPosition()
        {
            return transform.position;
        }

        public bool CanTarget()
        {
            return _owner.CanTarget();
        }

        public void ReceiveDamage(int damage, UnitBase attacker = null)
        {
            _owner.ReceiveDamage(damage, attacker);
        }
    }
}
