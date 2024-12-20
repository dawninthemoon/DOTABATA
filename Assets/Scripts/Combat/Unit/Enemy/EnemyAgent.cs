using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class EnemyAgent : MonoBehaviour
    {
        public event System.Action<Vector2> OnMovementRequested;
        public event System.Action<ITargetable> OnAttackRequested;
        public ITargetable CurrentTarget => _targeter.CurrentTarget;

        private EnemyTargeter _targeter;
        private EnemyUnit _self;

        private AIData _aiData = new();

        public void Initialize(EnemyTargeter targeter, EnemyUnit self, float attackRange)
        {
            _targeter = targeter;
            _self = self;
            _aiData.attackRange = attackRange;
        }

        public void Progress(StageManager stageManager)
        {
            PerformDetection(stageManager);
        }

        private void PerformDetection(StageManager stageManager) 
        {
            _targeter.FindTarget(stageManager.CharacterManager);

            var currentTarget = _targeter.CurrentTarget;
            if (Vector2.Distance(currentTarget.GetPosition(), _self.GetPosition()) < _aiData.attackRange)
            {
                OnAttackRequested?.Invoke(currentTarget);
            }
            else
            {
                Vector3 dir = (currentTarget.GetPosition() - _self.GetPosition()).normalized;
                OnMovementRequested?.Invoke(dir);
            }
        }
    }
}