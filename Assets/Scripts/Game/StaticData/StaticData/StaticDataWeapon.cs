using System.Collections;
using System.Collections.Generic;
using System;

namespace Game.StaticData
{
    public class StaticDataWeaponList : StaticDataWrapper<StaticDataWeapon>
    {
        private Dictionary<int, StaticDataWeapon> _dataByKey;
        public Dictionary<int, StaticDataWeapon> DataByKey => _dataByKey;

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
    public class StaticDataWeapon
    {
        public int keyIndex;
        public int magazineSize;
        public int spareMagazines;
        public float reloadTime;
        public int attack;
        public float attackSpeed;
        public float bulletSpeed;
        public float bulletRange;
        public float knockbackPower;
        public int spread;
    }
}
