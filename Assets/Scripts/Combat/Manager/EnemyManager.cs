using System.Collections;
using System.Collections.Generic;
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
    }
}
