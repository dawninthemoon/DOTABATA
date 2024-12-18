using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class EnemyAgent : MonoBehaviour
    {
        [SerializeField]
        private EnemyTargeter targeter;

        public event System.Action<Vector2> OnMovementRequested;
        public event System.Action<ITargetable> OnAttackRequested;
        public ITargetable CurrentTarget => targeter.CurrentTarget;

        private AIData _aiData = new();

        public void Initialize(EnemyUnit self, float attackRange)
        {
            _aiData.attackRange = attackRange;
        }

        public void Progress(StageManager stageManager)
        {
            PerformDetection(stageManager);
        }

        private void PerformDetection(StageManager stageManager) 
        {
            targeter.FindTarget(stageManager.CharacterManager);

            // Temp
            var currentTarget = targeter.CurrentTarget;
            if (currentTarget != null)
            {
                Vector3 dir = (currentTarget.GetPosition() - (Vector2)transform.position).normalized;
                OnMovementRequested?.Invoke(dir);
            }
        }
    }
}