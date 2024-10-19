using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterUnit : UnitBase
{
    [SerializeField]
    private MonsterRenderer monsterRenderer;
    [SerializeField]
    private float attackRange;
    private CharacterUnit _selectedTarget;

    public void Initialize(Vector2 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
        monsterRenderer.ChangeState(MonsterRenderer.State.Idle);
    }

    private void Update()
    {  
        if (_selectedTarget == null)
        {
            return;
        }


        if (Vector2.Distance(_selectedTarget.GetPosition(), GetPosition()) < attackRange)
        {
            monsterRenderer.ChangeState(MonsterRenderer.State.Idle);
        }
        else
        {
            Vector3 dir = (_selectedTarget.GetPosition() - GetPosition()).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;

            monsterRenderer.ChangeState(MonsterRenderer.State.Move);
        }

        monsterRenderer.UpdateAnimator(_selectedTarget);
    }

    public void SetTarget(CharacterUnit target)
    {
        _selectedTarget = target;
    }
}
