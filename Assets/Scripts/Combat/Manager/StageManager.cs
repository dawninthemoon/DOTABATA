using System.Collections;
using System.Collections.Generic;
using Game.StaticData;
using UnityEngine;

namespace Combat
{
    public class StageManager : MonoBehaviour
    {
        [System.Serializable]
        public struct SpawnArea
        {
            public Vector2 topLeft;
            public Vector2 bottomRight;
        }

        [SerializeField]
        private SpawnArea[] spawnAreaList;
        
        private int _currentWave;
        public int CurrentWave => _currentWave;
        public int WaveDisplay => _currentWave + 1;

        private bool _waveProcessing;
        private StaticDataStage _stage;
        private CombatManager _combatManager;

        public StageManager(CombatManager combatManager)
        {
            _combatManager = combatManager;
            _waveProcessing = false;
        }

        public void StartStage(int stageKey)
        {
            _stage = StaticDataManager.Instance.GetStageByKey(stageKey);
            StartWave(0);
        }

        public void StartWave(int waveKey)
        {
            _currentWave = waveKey;
            _waveProcessing = true;

            var selected = _combatManager.EnemyManager.SelectEnemy(_stage.stageKey, GetCurrentCost());
            foreach (int selectedKey in selected)
            {
                EnemyUnit enemyInstance = _combatManager.EnemyManager.CreateEnemy(selectedKey, _combatManager);
                enemyInstance.Initialize(selectedKey);
            }
        }

        public void ProcessWave()
        {
            if (!_waveProcessing)
            {
                return;
            }

            
        }

        public int GetCurrentCost()
        {
            int cost = _stage.initialCost + _stage.costIncrease * _currentWave;
            return cost;
        }
    }
}
