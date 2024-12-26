using System.Collections;
using System.Collections.Generic;
using Game.StaticData;
using UnityEngine;

namespace Combat
{
    public class WeaponBase
    {
        protected StaticDataWeapon _data;
        public StaticDataWeapon Data => _data;

        public virtual void Initialize(int weaponKey)
        {
            _data = StaticDataManager.Instance.GetWeaponDataByKey(weaponKey);
        }

        public virtual void OnAttack()
        {
            
        }
    }
}
