using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game.StaticData
{
    public class StaticDataSkillList : StaticDataWrapper<StaticDataSkill>
    {
        private Dictionary<int, StaticDataSkill> _dataByKey;
        public Dictionary<int, StaticDataSkill> DataByKey => _dataByKey;

        private Dictionary<int, StaticDataSkill> _dataByActionKey;
        public Dictionary<int, StaticDataSkill> DataByActionKey => _dataByActionKey;

        protected override void OnInitialized()
        {
            _dataByKey = new();
            _dataByActionKey = new();

            foreach (var data in _dataList)
            {
                if (!_dataByKey.ContainsKey(data.keyIndex))
                {
                    _dataByKey.Add(data.keyIndex, data);
                }

                if (!_dataByActionKey.ContainsKey(data.actionKey))
                {
                    _dataByActionKey.Add(data.actionKey, data);
                }
            }
        }
    }

    [Serializable]
    public class StaticDataSkill
    {
        public int keyIndex;
        public int actionKey;
        public float cooldown;
        public float value1;
        public float value2;
    }
}
