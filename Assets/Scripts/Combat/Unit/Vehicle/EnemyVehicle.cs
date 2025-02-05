using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class EnemyVehicle : MonoBehaviour
    {
        [field: SerializeField]
        public Transform HitPoint { get; private set; }

        private static readonly int MaxHP = 2;

        private int _currentHP;
        public int CurrentHP => _currentHP;

        private void Awake()
        {
            
        }
        
        public void Initialize()
        {
            _currentHP = MaxHP;
        }
    }
}