using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTest : MonoBehaviour
{
    private Vector3 _dir;
    private float _moveSpeed;
    private bool _initialized;

    private void Awake()
    {
        _initialized = false;
    }

    public void Initialize(Vector3 dir, float moveSpeed)
    {
        gameObject.SetActive(true);
        _dir = dir;
        _moveSpeed = moveSpeed;
        _initialized = true;
    }

    private void Update()
    {
        if (!_initialized)
        {
            return;
        }

        transform.position += _dir * _moveSpeed * Time.deltaTime;
    }
}
