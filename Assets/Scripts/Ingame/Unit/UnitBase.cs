using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    [SerializeField]
    protected float moveSpeed;

    public Vector2 GetPosition()
    {
        return transform.position;
    }
}
