using System.Collections;
using System.Collections.Generic;
using Combat.Actions;
using Cysharp.Threading.Tasks;
using Game.StaticData;
using UnityEngine;

namespace Combat
{
    public class WeaponBase
    {
        protected StaticDataWeapon _data;
        public StaticDataWeapon Data => _data;

        public virtual float BulletSpeed => _data.bulletSpeed;
        public virtual int MaxMagazines => _data.magazineSize;
        public virtual int SpareMagazines => _data.spareMagazines;
        public virtual float ReloadTime => _data.reloadTime;
        public virtual float SpreadDegree => _data.spreadDegree;

        protected CharacterUnit _owner;

        protected int _remainMagazines;
        protected int _remainSpareMagazines;
        private bool _isReloading;

        public virtual void Initialize(int weaponKey, CharacterUnit owner)
        {
            _owner = owner;
            _data = StaticDataManager.Instance.GetWeaponDataByKey(weaponKey);
            _remainMagazines = MaxMagazines;
            _remainSpareMagazines = SpareMagazines;
        }

        public virtual bool CanAttack()
        {
            return !_isReloading && _remainMagazines > 0;
        }

        public virtual void OnAttack()
        {
            --_remainMagazines;
        }

        public virtual void Fire(Vector2 position, ActionManager actionManager)
        {
            Vector2 mousePosition = Game.Utils.ExMouse.GetMouseWorldPosition();
            Vector2 dir = (mousePosition - position).normalized;

            var fireAction = actionManager.GetActionInstance(_owner, InputType.LeftClick) as FireAction;
            if (fireAction != null)
            {
                fireAction.SetDirection(dir);
                fireAction.Execute(_owner);
            }
        }

        public bool TryReload()
        {
            bool succeed = false;
            if (!_isReloading && _remainSpareMagazines > 0)
            {
                succeed = true;
                _isReloading = true;
                --_remainSpareMagazines;

                ReloadAsync().Forget();
            }

            return succeed;
        }

        private async UniTaskVoid ReloadAsync()
        {
            await UniTask.Delay(System.TimeSpan.FromSeconds(ReloadTime));

            _isReloading = false;            
            _remainMagazines = MaxMagazines;
        }
    }
}
