using System.Collections;
using System.Collections.Generic;
using Combat.Actions;
using Game.StaticData;
using UnityEngine;

namespace Combat
{
    public class CharacterUnit : UnitBase
    {
        [SerializeField]
        private CharacterRenderer characterRenderer;
        [SerializeField]
        private ColliderTargeter targeter;

        [SerializeField]
        private BulletTest testBulletPrefab;

        private InputStatus _inputStatus;
        public override TargetFaction Faction => TargetFaction.Character;

        public override int MaxHP => _data.hp;
        public override int AttackPower => _data.attack;
        public override float AttackSpeed => _data.attackSpeed;
        public override int Damage => AttackPower;
        public float MoveSpeed => _data.moveSpeed;

        private StaticDataCharacter _data;
        public StaticDataCharacter Data => _data;

        protected ActionManager _actionManager;

        protected override void Start()
        {
            testBulletPrefab.gameObject.SetActive(false);
        }

        public void SetActionManager(ActionManager actionManager)
        {
            _actionManager = actionManager;
        }

        public void Initialize(int characterKey)
        {
            gameObject.SetActive(true);

            _data = StaticDataManager.Instance.GetCharacterDataByKey(characterKey);

            InitializeStatus();

            targeter.Reset();
            targeter.SetDetectRange(100f);
        }

        private void InitializeStatus()
        {
            _health = MaxHP;
        }

        public void SetInput(InputStatus status)
        {
            _inputStatus = status;
        }

        public override void Progress()
        {
            base.Progress();

            var moveAction = _actionManager.GetActionInstance(this, InputType.Direction) as MovementAction;
            moveAction.SetDirection(_inputStatus.direction);
            moveAction.Execute(this);
            
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

            bullet.Initialize(dir, Damage, 1000f);

            OnAttack();
        }
    }
}