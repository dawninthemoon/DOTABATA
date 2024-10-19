using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    [SerializeField]
    protected float moveSpeed;
    [SerializeField]
    private float attackSpeed;

    private float _attackWaitTimer;

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public bool CanAttack()
    {
        return _attackWaitTimer <= 0f;
    }

    protected virtual void OnAttack()
    {
        _attackWaitTimer = 1f / attackSpeed;
    }

    protected virtual void ProcessAttackWaitTimer()
    {
        if (_attackWaitTimer > 0f)
        {
            _attackWaitTimer -= Time.deltaTime;
        }
    }
}
