using System.Collections;
using System.Collections.Generic;
using Game.StaticData;
using Game.Utils;
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
            public Vector2 offset;

            public Vector2 GetRandomPosition()
            {
                float x = Random.Range(topLeft.x, bottomRight.x);
                float y = Random.Range(bottomRight.y, topLeft.y);

                return new Vector2(x, y) + offset;
            }
        }

        [SerializeField]
        private SpawnArea[] spawnAreaList;
        [SerializeField]
        private VehicleTest vehicle;
        
        private int _currentWave;
        public int CurrentWave => _currentWave;
        public int WaveDisplay => _currentWave + 1;

        private bool _waveProcessing;
        private StaticDataStage _stage;
        private CombatManager _combatManager;

        public void SetDependency(CombatManager combatManager)
        {
            _combatManager = combatManager;
        }

        public void Initialize()
        {
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

                if (enemyInstance.Data.spawnType == SpawnType.Air)
                {
                    enemyInstance.transform.position = GetRandomSpawnPosition();
                }
                else
                {
                    enemyInstance.transform.position = GetRandomVehiclePosition();
                }
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

        public Vector2 GetRandomSpawnPosition()
        {
            var area = spawnAreaList.GetRandomElement();
            Vector2 randPos = area.GetRandomPosition();
            return randPos;
        }

        public Vector2 GetRandomVehiclePosition()
        {
            return vehicle.GetRandomPosition();
        }

    #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (spawnAreaList is null)
            {
                return;
            }

            Color defaultColor = Gizmos.color;
            Gizmos.color = Color.green;

            foreach (var area in spawnAreaList)
            {
                Vector2 center = new Vector2(area.bottomRight.x - area.topLeft.x, area.topLeft.y - area.bottomRight.y) / 2f;
                center.x += area.topLeft.x;
                center.y += area.bottomRight.y;

                Vector2 size = new Vector2(area.bottomRight.x - area.topLeft.x, area.topLeft.y - area.bottomRight.y);
                
                Gizmos.DrawWireCube(center + area.offset, size);
            }

            Gizmos.color = defaultColor;
        }
    #endif
    }
}
