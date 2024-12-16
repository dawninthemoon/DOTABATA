using System.Collections;
using System.Collections.Generic;
using Game.StaticData;
using UnityEngine;

namespace Combat
{
    public class CharacterUnit : UnitBase
    {
        [SerializeField]
        private CharacterRenderer characterRenderer;
        [SerializeField]
        private BulletTest testBulletPrefab;

        private InputStatus _inputStatus;
        
        private StaticDataCharacter _data;
        public StaticDataCharacter Data => _data;

        protected override float _attackSpeed => _data.attackSpeed;

        private void Start()
        {
            testBulletPrefab.gameObject.SetActive(false);
        }

        public void Initialize(int characterKey)
        {
            gameObject.SetActive(true);

            _data = StaticDataManager.Instance.GetCharacterDataByKey(characterKey);
        }

        public void SetInput(InputStatus status)
        {
            _inputStatus = status;
        }

        public override void Progress()
        {
            base.Progress();

            UpdatePosiiton();
            void UpdatePosiiton()
            {
                Vector3 moveVector = _inputStatus.direction.normalized * _data.moveSpeed; 
                transform.position += moveVector * Time.deltaTime;
            }

            bool fired = Input.GetMouseButtonDown(0) && CanAttack();
            if (fired)
            {
                Fire();
            }
            characterRenderer.ProcessInput(_inputStatus.direction, fired);

            ProcessAttackWaitTimer();
        }

        private void Fire()
        {
            var bullet = Instantiate(testBulletPrefab);
            bullet.transform.position = characterRenderer.BulletTransform.position;

            Vector2 mousePosition = Game.Utils.ExMouse.GetMouseWorldPosition();
            Vector2 dir = (mousePosition - (Vector2)transform.position).normalized;

            bullet.Initialize(dir, 1000f);

            OnAttack();
        }
    }
}