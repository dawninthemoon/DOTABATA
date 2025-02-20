using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class VehicleCore : MonoBehaviour, IInteractable, ITargetable
    {
        private int _armorPlate;
        private int _currentHP;

        public int ArmorPlate => _armorPlate;
        public int CurrentHP => _currentHP;

        public TargetFaction Faction { get => TargetFaction.AllyVehicleCore; }
        public event System.Action<ITargetable> OnDisappear;

        private static readonly int MaxHP = 4;

        public void Initialize()
        {
            _currentHP = MaxHP;
            _armorPlate = 0;
        }

        public Vector2 GetPosition()
        {
            return (Vector2)transform.position;   
        }

        public void Interact(CharacterUnit characterUnit)
        {
            characterUnit.UseArmorPlate(this);
        }

        public bool CanTarget()
        {
            return _currentHP > 0;
        }

        public void ReceiveDamage(int damage, UnitBase attacker = null)
        {
            int finalDamage = damage;

            if (_armorPlate > 0)
            {
                int blockedDamage = Mathf.Min(finalDamage, _armorPlate);
                _armorPlate -= blockedDamage;
                finalDamage -= blockedDamage;
            }

            _currentHP -= finalDamage;

            if (_currentHP <= 0)
            {
                Die();
            }
        }

        public void AddArmorPlate(int additionalArmor)
        {
            _armorPlate += additionalArmor;
        }

        private void Die()
        {
            _currentHP = 0;
            OnDisappear?.Invoke(this);

#region Test Code
            GetComponent<SpriteRenderer>().color = Color.gray;
#endregion
        }
    }
}