using System;
using System.Collections;
using System.Collections.Generic;

namespace Game.StaticData
{
    public class StaticDataCharacterActionList : StaticDataWrapper<StaticDataCharacterAction>
    {
        private MultiKeyDictionary<int, InputType, StaticDataCharacterAction> _dataDictionary;
        public MultiKeyDictionary<int, InputType, StaticDataCharacterAction> DataDictionary => _dataDictionary;

        protected override void OnInitialized()
        {
            _dataDictionary = new();
            foreach (var data in _dataList)
            {
                if (!_dataDictionary.ContainsKey(data.characterKey, data.inputType))
                {
                    _dataDictionary.Add(data.characterKey, data.inputType, data);
                }
            }
        }
    }

    [Serializable]
    public class StaticDataCharacterAction
    {
        public int keyIndex;
        public ActionType actionType;
        public InputType inputType;
        public int characterKey;
    }
}
