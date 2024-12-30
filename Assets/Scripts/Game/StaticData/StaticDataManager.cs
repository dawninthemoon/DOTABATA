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
        private StaticDataWeaponList _weaponList = new();
        private StaticDataStageList _stageList = new();

        public void Initialize()
        {
            string jsonString = Resources.Load<TextAsset>(BasePath).ToString();
            JObject jObject = JObject.Parse(jsonString);

            _characterDataList.LoadData(jObject);
            _monsterDataList.LoadData(jObject);
            _characterActionList.LoadData(jObject);
            _weaponList.LoadData(jObject);
            _stageList.LoadData(jObject);
        }

    #region Get
        public StaticDataCharacter GetCharacterDataByKey(int characterKey)
        {
            return _characterDataList.DataByKey.TryGetValue(characterKey, out var data) ? data : null;
        }

        public StaticDataEnemy GetEnemyDataByKey(int enemyKey)
        {
            return _monsterDataList.DataByKey.TryGetValue(enemyKey, out var data) ? data : null;
        }

        public List<StaticDataEnemy> GetEnemyList()
        {
            return _monsterDataList.DataList;
        }

        public StaticDataCharacterAction GetCharacterActionData(int characterKey, InputType inputType)
        {
            return _characterActionList.DataDictionary.TryGetValue(characterKey, inputType, out var data) ? data : null;
        }

        public StaticDataWeapon GetWeaponDataByKey(int weaponKey)
        {
            return _weaponList.DataByKey.TryGetValue(weaponKey, out var data) ? data : null;
        }

        public StaticDataStage GetStageByKey(int stageKey)
        {
            return _stageList.DataByKey.TryGetValue(stageKey, out var data) ? data : null;
        }
    #endregion
    }
}
