using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StaticData;

namespace Combat
{
    public class EnemyUnit : UnitBase
    {
        protected StaticDataEnemy _data;
        public StaticDataEnemy Data => _data;

        public void Initialize(int enemyKey)
        {
            _data = StaticDataManager.Instance.GetEnemyDataByKey(enemyKey);
        }
    }
}