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

        private InputStatus _inputStatus;
        public override TargetFaction Faction => TargetFaction.Character;

        public override int MaxHP => _data.hp;
        public override int AttackPower => _weapon.Data.attack;
        public override float AttackSpeed => _weapon.Data.attackSpeed;
        public override int Damage => AttackPower;
        public float MoveSpeed => _data.moveSpeed;

        private StaticDataCharacter _data;
        public StaticDataCharacter Data => _data;

        protected WeaponBase _weapon;
        public WeaponBase Weapon => _weapon;

        public Transform BulletTransform => characterRenderer.BulletTransform;
        protected ActionManager _actionManager;

        public void SetActionManager(ActionManager actionManager)
        {
            _actionManager = actionManager;
        }

        public void Initialize(int characterKey)
        {
            gameObject.SetActive(true);

            _data = StaticDataManager.Instance.GetCharacterDataByKey(characterKey);
            _weapon = new WeaponBase();
            _weapon.Initialize(_data.weaponKey, this);

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
            if (IsDie())
            {
                return;
            }

            base.Progress();

            var moveAction = _actionManager.GetActionInstance(this, InputType.Direction) as MovementAction;
            moveAction.SetDirection(_inputStatus.direction);
            moveAction.Execute(this);
                
            CharacterRenderer.ArmState armState = CharacterRenderer.ArmState.Idle;
            if (_inputStatus.mouseDown)
            {
                if (_weapon.CanAttack())
                {
                    if (CanAttack())
                    {
                        armState = CharacterRenderer.ArmState.Fire;
                        _weapon.Fire(GetPosition(), _actionManager);
                    }
                }
                else
                {
                    if (_weapon.TryReload())
                    {
                        armState = CharacterRenderer.ArmState.Reload;
                    }
                }
            }
            characterRenderer.ProcessInput(_inputStatus.direction, armState);

            ProcessAttackWaitTimer();
        }

        public override void ReceiveDamage(int damage, UnitBase attacker = null)
        {
            base.ReceiveDamage(damage, attacker);
        }

        protected override void OnDie(UnitBase attacker)
        {
            characterRenderer.ChangeToDown();
        }

        public virtual bool CanAttackWithWeapon()
        {
            return _weapon.CanAttack() && CanAttack();
        }

        public override void OnAttack()
        {
            base.OnAttack();
            _weapon.OnAttack();
        }
    }
}