using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Core;
using Game.StaticData;
using Game.Utils;
using UnityEngine;

namespace Combat
{
    public class EnemyManager : MonoBehaviour
    {
        private Dictionary<int, ObjectPool<EnemyUnit>> _enemyObjectPool;
        private List<EnemyUnit> _enemyList;

        private void Awake()
        {
            _enemyList = new();
            _enemyObjectPool = new();
        }

        public void Initialize(List<int> enemyList)
        {
            foreach (int enemyKey in enemyList)
            {
                if (!_enemyObjectPool.ContainsKey(enemyKey))
                {
                    int selectedKey = enemyKey;
                    ObjectPool<EnemyUnit> enemyPool = new ObjectPool<EnemyUnit>(
                        10,
                        () => CreateEnemy(selectedKey),
                        OnEnemyActive,
                        OnEnemyDisable
                    );

                    _enemyObjectPool.Add(enemyKey, enemyPool);
                }
            }
        }

        public EnemyUnit CreateEnemy(int enemyKey, CombatManager combatManager)
        {
            if (!_enemyObjectPool.TryGetValue(enemyKey, out var objectPool))
            {
                return null;
            }
            EnemyUnit instance = objectPool.GetObject();

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

        public void OnEnemyDie(EnemyUnit enemy)
        {
            ReleaseEnemy(enemy);
        }

        public int GetActiveEnemyCostSum()
        {
            int sum = 0;
            foreach (var enemy in _enemyList)
            {
                sum += enemy.Data.spawnCost;
            }
            return sum;
        }

        private void ReleaseEnemy(EnemyUnit enemy)
        {
            if (_enemyList.Contains(enemy))
            {
                _enemyList.Remove(enemy);
            }

            if (_enemyObjectPool.TryGetValue(enemy.Data.keyIndex, out var objectPool))
            {
                objectPool.ReturnObject(enemy);
            }
        }

        private EnemyUnit CreateEnemy(int enemyKey)
        {
            var prefab = AssetLoader.Instance.GetComponentObject<EnemyUnit>($"{AssetLoader.EnemyPathBase}Enemy{enemyKey:D2}");
            EnemyUnit instance = Instantiate(prefab);
            return instance;
        }

        private void OnEnemyActive(EnemyUnit enemy)
        {
            enemy.gameObject.SetActive(true);
        }

        private void OnEnemyDisable(EnemyUnit enemy)
        {
            enemy.gameObject.SetActive(false);
        }
    }
}
