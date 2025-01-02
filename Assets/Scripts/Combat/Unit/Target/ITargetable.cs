using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public interface ITargetable 
    {
        public TargetFaction Faction { get; }
        public Vector2 GetPosition();
        public event System.Action<UnitBase> OnDisappear;
        public bool CanTarget();
    }
}
