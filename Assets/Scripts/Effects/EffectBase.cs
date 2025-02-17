using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Utils;

public class EffectBase : MonoBehaviour
{
    public SpriteDirection defaultDirection = SpriteDirection.Right;

    public event Action onStop;
    public event Action onRelease;

    /// <summary>
    /// 이펙트 종료. 종료 애니메이션까지 완료하면 Release() 호출
    /// </summary>
    public virtual void Stop()
    {
        onStop?.Invoke();
    }

    public virtual void Rotate(float angle)
    {
        transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public virtual void SetSpriteDirection(SpriteDirection direction)
    {
        transform.SetLocalScaleX(direction == defaultDirection ? Mathf.Abs(transform.localScale.x) : -Mathf.Abs(transform.localScale.x));
    }

    public void SetAutoRelease(float time)
    {
        StartCoroutine(AutoRelease(time));
    }

    IEnumerator AutoRelease(float t)
    {
        yield return new WaitForSeconds(t);
        Release();
    }

    public virtual void Release()
    {
        // TODO: ObjectPool 적용
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
