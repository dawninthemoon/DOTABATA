using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class EnemyManager_Legacy : MonoBehaviour
    {
        [SerializeField]
        private VehicleTest vehicleArea;
        [SerializeField]
        private int enemySpawnCount;
        [SerializeField]
        private MonsterUnit_Legacy testPrefab;
        [SerializeField]
        private CharacterUnit testUnit;

        [ContextMenu("StartWave")]
        public void StartWave()
        {
            for (int i = 0; i < enemySpawnCount; ++i)
            {
                CreateEnemy();
            }
            void CreateEnemy()
            {
                Vector2 pos = vehicleArea.GetRandomArea();
                var monster = Instantiate(testPrefab);
                monster.Initialize(pos);
                monster.SetTarget(testUnit);
            }
        }
    }
}