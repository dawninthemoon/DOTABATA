using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class StageManager : MonoBehaviour
    {
        private EnemyManager _enemyManager;
        private EffectManager _effectManager;
        private WaveManager _waveManager;

        private void Awake()
        {
            _enemyManager = GetComponent<EnemyManager>();
            _effectManager = GetComponent<EffectManager>();
            _waveManager = new();
        }

        public void Initialize()
        {
            
        }
    }
}
