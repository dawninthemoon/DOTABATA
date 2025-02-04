using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class VehicleCore : MonoBehaviour, IInteractable, ITargetable
    {
        private int _currentHP;
        public int CurrentHP => _currentHP;

        public TargetFaction Faction { get => TargetFaction.Character; }
        public event System.Action<ITargetable> OnDisappear;

        private static readonly int MaxHP = 4;

        public void Initialize()
        {
            _currentHP = MaxHP;
        }

        public Vector2 GetPosition()
        {
            return (Vector2)transform.position;   
        }

        public void Interact(CharacterUnit characterUnit)
        {

        }

        public bool CanTarget()
        {
            return _currentHP > 0;
        }

        public void ReceiveDamage(int damage, UnitBase attacker = null)
        {
            _currentHP -= 1;

            if (_currentHP <= 0)
            {
                Die();
            }
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