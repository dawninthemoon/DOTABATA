using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Combat
{
    public class MonsterUnit_Legacy : UnitBase
    {
        [SerializeField]
        private EnemyRenderer monsterRenderer;
        [SerializeField]
        private float attackRange;
        private CharacterUnit _selectedTarget;

        private StaticDataMonster _data;

        public void Initialize(Vector2 position)
        {
            gameObject.SetActive(true);
            transform.position = position;
            monsterRenderer.ChangeState(EnemyRenderer.State.Idle);
        }

        private void Update()
        {  
            if (_selectedTarget == null)
            {
                return;
            }

            if (Vector2.Distance(_selectedTarget.GetPosition(), GetPosition()) < attackRange)
            {
                monsterRenderer.ChangeState(EnemyRenderer.State.Idle);
            }
            else
            {
                Vector3 dir = (_selectedTarget.GetPosition() - GetPosition()).normalized;
                transform.position += dir * moveSpeed * Time.deltaTime;

                monsterRenderer.ChangeState(EnemyRenderer.State.Move);
            }

            monsterRenderer.UpdateAnimator(_selectedTarget);
        }

        public void SetTarget(CharacterUnit target)
        {
            _selectedTarget = target;
        }
    }
}