using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public interface ITargetable 
    {
        public TargetFaction Faction { get; }
        public event System.Action<ITargetable> OnDisappear;

        public Vector2 GetPosition();
        public bool CanTarget();
        public void ReceiveDamage(int damage, UnitBase attacker = null);
    }
}
