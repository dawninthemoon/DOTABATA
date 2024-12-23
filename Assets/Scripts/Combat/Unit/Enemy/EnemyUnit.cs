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
        protected EnemyAgent _agent;

        protected StaticDataEnemy _data;
        public StaticDataEnemy Data => _data;

        public override int MaxHP => _data.hp;
        public override int AttackPower => _data.attack;
        public override float AttackSpeed => _data.attackSpeed;
        public override int Damage => AttackPower;

        public override TargetFaction Faction => TargetFaction.Enemy;

        private void Awake()
        {
            _agent = GetComponent<EnemyAgent>();
            _agent.Initialize(targeter, this, _data.attackRange);
        }

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
            enemyRenderer.ChangeState(EnemyRenderer.State.Idle);

            InitializeStatus();
        }

        private void InitializeStatus()
        {
            _health = MaxHP;
        }

        private void OnMovementRequested(Vector2 dir)
        {
            transform.position += (Vector3)dir * _data.moveSpeed * Time.deltaTime;

            enemyRenderer.ChangeState(EnemyRenderer.State.Move);
        }

        private void OnAttackRequested(ITargetable target)
        {
            enemyRenderer.ChangeState(EnemyRenderer.State.Idle);
        }

        private void Update()
        {  
            _agent.Progress(_combatManager);
            enemyRenderer.UpdateAnimator(targeter.CurrentTarget);
        }
    }
}