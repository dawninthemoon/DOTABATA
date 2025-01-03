using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StaticData;

namespace Combat
{
    public class EnemyUnit : UnitBase, ITargetable
    {
        [SerializeField]
        private EnemyTargeter targeter;
        [SerializeField]
        private EnemyRenderer enemyRenderer;
        [SerializeField]
        protected EnemyAgent _agent;
        protected Vector2 _direction;

        protected StaticDataEnemy _data;
        public StaticDataEnemy Data => _data;

        public override int MaxHP => _data.hp;
        public override int AttackPower => _data.attack;
        public override float AttackSpeed => _data.attackSpeed;
        public override int Damage => AttackPower;

        public Vector2 Direction => _direction;
        public override TargetFaction Faction => TargetFaction.Enemy;
        private IEnemyAttack _attackScript;

        private void OnEnable()
        {
            _agent.OnMovementRequested += OnMovementRequested;
            _agent.OnAttackRequested += OnAttackRequested;
        }

        private void OnDisable()
        {
            _agent.OnMovementRequested -= OnMovementRequested;
            _agent.OnAttackRequested -= OnAttackRequested;
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
            _agent.Initialize(targeter, this, _data.attackRange);
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
            transform.position += (Vector3)_direction * _data.moveSpeed * Time.deltaTime;

            enemyRenderer.ChangeState(EnemyRenderer.State.Move, false);
        }

        private void OnAttackRequested(ITargetable target)
        {
            enemyRenderer.ChangeState(EnemyRenderer.State.Attack, true);
            _attackScript.RequestAttack(this, _combatManager);
            OnAttack();
        }

        private void Update()
        {  
            ProcessAttackWaitTimer();

            _agent.Progress(_combatManager);
            enemyRenderer.UpdateAnimator(targeter.CurrentTarget);
        }

        protected override void OnDie(UnitBase attacker)
        {
            _combatManager.EnemyManager.OnEnemyDie(this);
        }
    }
}