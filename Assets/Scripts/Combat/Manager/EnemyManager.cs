using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.StaticData;
using Game.Utils;
using UnityEngine;

namespace Combat
{
    public class EnemyManager : MonoBehaviour
    {
        [Header("Temp"), SerializeField]
        private EnemyUnit enemyPrefab_Test;

        private ObjectPool<EnemyUnit> _enemyObjectPool;
        private List<EnemyUnit> _enemyList;

        private void Awake()
        {
            _enemyList = new();
        }

        public EnemyUnit CreateEnemy(int enemyKey, CombatManager combatManager)
        {
            EnemyUnit instance = Instantiate(enemyPrefab_Test);

            instance.SetDependency(combatManager);
            instance.Initialize(enemyKey);

            _enemyList.Add(instance);

            return instance;
        }

        public List<int> SelectEnemy(int stageKey, int cost)
        {
            var enemyList = StaticDataManager.Instance.GetStageByKey(stageKey).monsters;
            int remainCost = cost;
            int costMin = int.MaxValue;
            foreach (int enemyKey in enemyList)
            {
                var enemyData = StaticDataManager.Instance.GetEnemyDataByKey(enemyKey);
                costMin = Mathf.Min(costMin, enemyData.spawnCost);
            }
            
            List<int> selected = new();
            while (remainCost >= costMin)
            {
                int randKey = enemyList.GetRandomElement();
                var randEnemy = StaticDataManager.Instance.GetEnemyDataByKey(randKey);
                if (remainCost < randEnemy.spawnCost)
                {
                    continue;
                }

                remainCost -= randEnemy.spawnCost;
                selected.Add(randKey);
            }

            return selected;
        }
    }
}
