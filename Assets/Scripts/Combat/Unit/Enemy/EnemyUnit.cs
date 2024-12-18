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
        protected EnemyAgent _agent;

        protected StaticDataEnemy _data;
        public StaticDataEnemy Data => _data;

        public override TargetFaction Faction => TargetFaction.Enemy;

        private void Awake()
        {
            _agent = GetComponent<EnemyAgent>();
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
            _agent.Progress(_stageManager);

        /*
            var selectedTarget = _agent.CurrentTarget;
            if (selectedTarget == null)
            {
                return;
            }

            if (Vector2.Distance(selectedTarget.GetPosition(), GetPosition()) < _data.attackRange)
            {
                enemyRenderer.ChangeState(EnemyRenderer.State.Idle);
            }
            else
            {
                Vector3 dir = (selectedTarget.GetPosition() - GetPosition()).normalized;
                transform.position += dir * Time.deltaTime;

                enemyRenderer.ChangeState(EnemyRenderer.State.Move);
            }

            enemyRenderer.UpdateAnimator(selectedTarget);
        */
        }
    }
}