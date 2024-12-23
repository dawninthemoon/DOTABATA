using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Game.StaticData
{
    public class StaticDataManager : Utils.Singleton<StaticDataManager>
    {
        private static readonly string BasePath = "StaticData/StaticData";

        private StaticDataCharacterList _characterDataList = new();
        private StaticDataEnemyList _monsterDataList = new();
        private StaticDataCharacterActionList _characterActionList = new();

        public void Initialize()
        {
            string jsonString = Resources.Load<TextAsset>(BasePath).ToString();
            JObject jObject = JObject.Parse(jsonString);

            _characterDataList.LoadData(jObject);
            _monsterDataList.LoadData(jObject);
            _characterActionList.LoadData(jObject);
        }

        public StaticDataCharacter GetCharacterDataByKey(int characterKey)
        {
            return _characterDataList.DataByKey.TryGetValue(characterKey, out var data) ? data : null;
        }

        public StaticDataEnemy GetEnemyDataByKey(int enemyKey)
        {
            return _monsterDataList.DataByKey.TryGetValue(enemyKey, out var data) ? data : null;
        }

        public StaticDataCharacterAction GetCharacterActionData(int characterKey, InputType inputType)
        {
            return _characterActionList.DataDictionary.TryGetValue(characterKey, inputType, out var data) ? data : null;
        }
    }
}
