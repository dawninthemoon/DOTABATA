using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Combat
{
    public class PlayerTurret : TurretBase, IProgressiveInteractable
    {
        private bool _isInteracting;
        private float _interactingTime;
        private bool _isProcessing;
        private int _remainShells;

        private static readonly float TargetInteractingTime = 2f;

        private CombatManager _combatManager;

        [SerializeField]
        private float angleRate;

        public void Initialize()
        {
            _isInteracting = false;
            _interactingTime = 0f;
            _isProcessing = false;
        }

        public void SetDependency(CombatManager combatManager)
        {
            _combatManager = combatManager;
        }

        public void InteractProgress(CharacterUnit characterUnit)
        {
            if (_isProcessing)
            {
                return;
            }
            
            _isInteracting = true;
            _interactingTime += Time.deltaTime;

            if (_interactingTime >= TargetInteractingTime)
            {
                StartAttack();
            }
        }

        public void InteractEnd(CharacterUnit characterUnit)
        {
            _isInteracting = false;
            _interactingTime = 0f;
        }

        public void StartAttack()
        {
            _isProcessing = true;
            _remainShells = 1;
        }
        
        private void Update()
        {
            if (!_isProcessing)
            {
                return;
            }

            var target = _combatManager.EnemyManager.EnemyVehicle;
            if (target != null && target.CurrentHP >= 0)
            {
                ProcessAttack(target);
            }
        }

        private void ProcessAttack(EnemyVehicle target)
        {
            Vector2 diff = target.HitPoint.position - transform.position;
            float radian = Mathf.Atan2(diff.y, diff.x);
            float degree = radian * Mathf.Rad2Deg;
            float myDegree = transform.localRotation.z;

            if (Mathf.Abs(myDegree - degree) > 5f)
            {
                float rotateAmount = -angleRate * Time.deltaTime;
                transform.Rotate(0f, 0f, rotateAmount);
            }
        }
    }
}