using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Combat.Actions
{
    public class YuriActiveSkill : ActiveSkillAction
    {
        private bool _isExecuting = false;
        private List<EnemyUnit> _targetList = new();

        public override void Execute(CharacterUnit actor)
        {
            base.Execute(actor);
            _isExecuting = true;
            TargetProcess().Forget();
        }

        private async UniTaskVoid TargetProcess()
        {
            float targetDelay = 0.2f;
            float timeAgo = 0f;
            float maxTimeAgo = 2f;

            _targetList.Clear();

            var enemies = _combatManager.EnemyManager.GetEnemyArray();

            foreach (var enemy in enemies)
            {
                if (_targetList.Count > Data.value1 || timeAgo > maxTimeAgo)
                {
                    break;
                }

                if (!TryAddTarget(enemy))
                {
                    continue;
                }

                await UniTask.Delay(System.TimeSpan.FromSeconds(targetDelay));

                timeAgo += targetDelay;
            }
            
            for (int i = 0; i < _targetList.Count; ++i)
            {
                if (_targetList[i].IsDie())
                {
                    _targetList.RemoveAt(i--);
                    continue;
                }
            }

            _isExecuting = false;
        }

        private bool TryAddTarget(EnemyUnit enemy)
        {
            if (!enemy.IsDie())
            {
                enemy.GetComponentInChildren<SpriteRenderer>().color = Color.red;

                _targetList.Add(enemy);
                return true;
            }

            return false;
        }

        public override bool CanMove()
        {
            return !_isExecuting;
        }

        public override bool CanAttack()
        {
            return !_isExecuting;
        }

        public override bool CanInteract()
        {
            return !_isExecuting;
        }
    }
}