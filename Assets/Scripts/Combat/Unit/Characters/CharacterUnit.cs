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
        private CharacterUI characterUI;
        [SerializeField]
        private ColliderTargeter targeter;
        [SerializeField]
        private RaycastController raycastController;
        [SerializeField]
        private InteractiveArea interactiveArea;

        private InputStatus _inputStatus;
        public override TargetFaction Faction => TargetFaction.Character;

        private int _armorPlate;
        private int _instanceID;

        public override int MaxHP => _data.hp * 100000;
//        public override int MaxHP => _data.hp;
        public override int AttackPower => _weapon.Data.attack;
        public override float AttackSpeed => _weapon.Data.attackSpeed;
        public override int Damage => AttackPower;
        public float MoveSpeed => _data.moveSpeed;
        public int ArmorPlate => _armorPlate;
        public int InstanceID => _instanceID;

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

        public void Initialize(int characterKey, int instanceID)
        {
            gameObject.SetActive(true);

            _instanceID = instanceID;

            _data = StaticDataManager.Instance.GetCharacterDataByKey(characterKey);
            _weapon = new WeaponBase();
            _weapon.Initialize(_data.weaponKey, this);

            InitializeStatus();

            interactiveArea.Reset();
            targeter.Reset();
            targeter.SetDetectRange(100f);

            raycastController.Initialize(GetComponent<BoxCollider2D>().size * 0.5f);
        }

        private void InitializeStatus()
        {
            _health = MaxHP;
        }

        public void SetInput(InputStatus status)
        {
            _inputStatus = status;
        }

        public void Move(Vector2 moveAmount)
        {
            moveAmount = raycastController.ProcessMovement(GetPosition(), moveAmount);
            AddPosition(moveAmount);
        }

        public override void Progress()
        {
            if (IsDie())
            {
                return;
            }

            ProcessAttackWaitTimer();

            characterUI.SetInteractionState(interactiveArea.Selected != null);

            base.Progress();

            var interactionInstance = _actionManager.GetActionInstance(this, InputType.Interact) as Interaction;
            interactionInstance.OverlapColliders(this);

            if (interactionInstance.CanInteract())
            {
                bool isInteracting = false;

                if (_inputStatus.interaction)
                {
                    isInteracting = true;
                    interactionInstance.Execute(this);
                }
                if (_inputStatus.interactionHolding)
                {
                    isInteracting = true;
                    interactionInstance.Progress(this);
                }
                if (_inputStatus.interactionEnd)
                {
                    isInteracting = true;
                    interactionInstance.InteractionEnd(this);
                }

                if (isInteracting)
                {
                    characterRenderer.ChangeToInteraction();
                    return;
                }
            }
            
            var moveAction = _actionManager.GetActionInstance(this, InputType.Direction) as MovementAction;
            moveAction.SetDirection(_inputStatus.direction);
            moveAction.Execute(this);
            
            CharacterRenderer.ArmState armState = CharacterRenderer.ArmState.Idle;
            bool reloadSucceed = false;
            if (_inputStatus.mouse0)
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
                    reloadSucceed = _weapon.TryReload();
                }
            }

            if (_inputStatus.reload)
            {
                reloadSucceed = _weapon.TryReload();
            }

            if (reloadSucceed)
            {
                armState = CharacterRenderer.ArmState.Reload;
                characterUI.OnReloadStart();
            }
            else if (!_weapon.IsReloading)
            {
                characterUI.OnReloadEnd();
            }

            characterRenderer.ProcessInput(_inputStatus.direction, armState);
        }

        public override void ReceiveDamage(int damage, UnitBase attacker = null)
        {
            base.ReceiveDamage(damage, attacker);
        }

        public void AddArmorPlate(int additionalArmor)
        {
            _armorPlate += additionalArmor;
        }

        public void UseArmorPlate(VehicleCore core)
        {
            int usedArmorPlate = Mathf.Min(_armorPlate, 4);
            _armorPlate -= usedArmorPlate;

            core.AddArmorPlate(usedArmorPlate);
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