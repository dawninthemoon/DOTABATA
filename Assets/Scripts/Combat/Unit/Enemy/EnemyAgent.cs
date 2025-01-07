using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class EnemyAgent : MonoBehaviour
    {
        public event System.Action<Vector2> OnMovementRequested;
        public event System.Action OnAttackRequested;

        [SerializeField]
        private ContextSolver movementSolver;
        [SerializeField]
        private EnemyTargeter targeter;
        [SerializeField]
        private ObstacleDetector obstacleDetector;
        [SerializeField]
        private List<SteeringBehaviour> steeringBehaviours;

        private EnemyUnit _self;
        private AIData _aiData = new();

        public ITargetable DetectedTarget => _aiData.detectedTarget;
        public ITargetable SelectedTarget => _aiData.selectedTarget;

        public void Initialize(EnemyUnit self, float attackRange, float agentRadius)
        {
            _self = self;

            _aiData.agentRadius = agentRadius;
            _aiData.attackRange = attackRange;
            _aiData.detectRange = 2000f;

            targeter.Initialize(_aiData);
        }

        public void Progress(CombatManager combatManager)
        {
            PerformDetection(combatManager);
        }

        private void PerformDetection(CombatManager combatManager) 
        {
            targeter.FindTarget(combatManager.CharacterManager);
            obstacleDetector.Detect(_aiData);

            var detectedTarget = _aiData.detectedTarget;
            if (detectedTarget == null)
            {
                return;
            }

            bool isInAttackRange = Vector2.Distance(detectedTarget.GetPosition(), _self.GetPosition()) < _aiData.attackRange;
            if (isInAttackRange)
            {
                if (_self.CanAttack())
                {
                    _aiData.selectedTarget = detectedTarget;
                    OnAttackRequested?.Invoke();
                }
            }
            else
            {
                Vector2 solvedDirection = Vector2.zero;
                if (_aiData.detectedTarget != null) 
                {
                    solvedDirection = movementSolver.GetDirectionToMove(steeringBehaviours, _aiData);
                }
                OnMovementRequested?.Invoke(solvedDirection);
            }
        }
    }
}