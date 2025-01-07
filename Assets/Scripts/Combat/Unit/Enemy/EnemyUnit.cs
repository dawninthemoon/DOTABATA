using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StaticData;

namespace Combat
{
    public class EnemyUnit : UnitBase, ITargetable
    {
        [SerializeField]
        private EnemyRenderer enemyRenderer;
        [SerializeField]
        protected EnemyAgent agent;
        [SerializeField]
        private RaycastController raycastController;

        protected Vector2 _direction;

        protected StaticDataEnemy _data;
        public StaticDataEnemy Data => _data;

        public override int MaxHP => _data.hp;
        public override int AttackPower => _data.attack;
        public override float AttackSpeed => _data.attackSpeed;
        public override int Damage => AttackPower;

        public Vector2 Direction => _direction;
        public ITargetable SelectedTarget => agent.SelectedTarget;
        public override TargetFaction Faction => TargetFaction.Enemy;
        private IEnemyAttack _attackScript;

        private void OnEnable()
        {
            agent.OnMovementRequested += OnMovementRequested;
            agent.OnAttackRequested += OnAttackRequested;
        }

        private void OnDisable()
        {
            agent.OnMovementRequested -= OnMovementRequested;
            agent.OnAttackRequested -= OnAttackRequested;
        }

        public void Initialize(int enemyKey)
        {
            _data = StaticDataManager.Instance.GetEnemyDataByKey(enemyKey);
            gameObject.SetActive(true);
            enemyRenderer.Reset();

            InitializeStatus();

            if (_data.enemyAttackType == EnemyAttackType.Melee)
            {
                _attackScript = new EnemyMeleeAttack();
            }
            else
            {
                _attackScript = new EnemyRangeAttack();
            }

            float radius = GetComponent<CircleCollider2D>().radius;
            agent.Initialize(this, _data.attackRange, radius);
            raycastController.Initialize(Vector2.one * radius);
        }

        private void InitializeStatus()
        {
            _health = MaxHP;
        }

        private void OnMovementRequested(Vector2 dir)
        {
            if (enemyRenderer.CurrentState == EnemyRenderer.State.Attack)
            {
                return;
            }

            _direction = dir;

            Vector2 moveAmount = _direction * _data.moveSpeed * Time.deltaTime;
            moveAmount = raycastController.ProcessMovement(GetPosition(), moveAmount);
            
            transform.position += (Vector3)moveAmount;

            enemyRenderer.ChangeState(EnemyRenderer.State.Move, false);
        }

        private void OnAttackRequested()
        {
            if (SelectedTarget == null)
            {
                return;
            }

            enemyRenderer.ChangeState(EnemyRenderer.State.Attack, true);
            _attackScript.RequestAttack(this, _combatManager);

            Vector2 dir = (SelectedTarget.GetPosition() - GetPosition()).normalized;
            enemyRenderer.UpdateAnimator(dir, agent.SelectedTarget);

            OnAttack();
        }

        private void Update()
        {  
            ProcessAttackWaitTimer();

            agent.Progress(_combatManager);
            enemyRenderer.UpdateAnimator(Direction, agent.DetectedTarget);
        }

        protected override void OnDie(UnitBase attacker)
        {
            _combatManager.EnemyManager.OnEnemyDie(this);
        }
    }
}