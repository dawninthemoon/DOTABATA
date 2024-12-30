using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game.StaticData
{
    public class StaticDataStageList : StaticDataWrapper<StaticDataStage>
    {
        private Dictionary<int, StaticDataStage> _dataByKey;
        public Dictionary<int, StaticDataStage> DataByKey => _dataByKey;

        protected override void OnInitialized()
        {
            _dataByKey = new();
            foreach (var data in _dataList)
            {
                if (!_dataByKey.ContainsKey(data.stageKey))
                {
                    _dataByKey.Add(data.stageKey, data);
                }
            }
        }
    }

    [Serializable]
    public class StaticDataStage
    {
        public int stageKey;
        public int initialCost;
        public int costIncrease;
        public List<int> monsters;
    }
}
