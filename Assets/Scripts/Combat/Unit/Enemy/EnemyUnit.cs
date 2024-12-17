using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StaticData;

namespace Combat
{
    public class EnemyUnit : UnitBase, ITargetable
    {
        protected StaticDataEnemy _data;
        public StaticDataEnemy Data => _data;

        public override TargetFaction Faction => TargetFaction.Enemy;

        public void Initialize(int enemyKey)
        {
            _data = StaticDataManager.Instance.GetEnemyDataByKey(enemyKey);
        }
    }
}