using System;
using System.Collections;
using System.Collections.Generic;

namespace Game.StaticData
{
    public class StaticDataCharacterList : StaticDataWrapper<StaticDataCharacter>
    {
        private Dictionary<int, StaticDataCharacter> _dataByKey;
        public Dictionary<int, StaticDataCharacter> DataByKey => _dataByKey;

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
    public class StaticDataCharacter
    {
        public int keyIndex;
        public int hp;
        public int attack;
        public float moveSpeed;
        public float attackSpeed;
    }
}