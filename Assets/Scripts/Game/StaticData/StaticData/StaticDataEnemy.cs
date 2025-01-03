using System;
using System.Collections;
using System.Collections.Generic;

namespace Game.StaticData
{
    public enum MonsterMoveType
    {
        Ground,
        Air,
    }

    public enum SpawnType
    {
        Air,
        Vehicle,
    }

    public enum EnemyAttackType
    {
        Melee,
        Range,
    }

    public class StaticDataEnemyList : StaticDataWrapper<StaticDataEnemy>
    {
        private Dictionary<int, StaticDataEnemy> _dataByKey;
        public Dictionary<int, StaticDataEnemy> DataByKey => _dataByKey;

        protected override void OnInitialized()
        {
            _dataByKey = new();
            foreach (var data in _dataList)
            {
                if (!_dataByKey.ContainsKey(data.keyIndex))
                {
                    _dataByKey.Add(data.keyIndex, data);
                }
            }
        }
    }

    [Serializable]
    public class StaticDataEnemy
    {
        public int keyIndex;
        public int hp;
        public int attack;
        public float moveSpeed;
        public float attackSpeed;
        public MonsterMoveType moveType;
        public float attackRange;
        public int spawnCost;
        public SpawnType spawnType;
        public EnemyAttackType enemyAttackType;
    }
}