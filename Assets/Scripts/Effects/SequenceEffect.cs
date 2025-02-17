using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SequenceEffect : EffectBase
{
    [SerializeField]
    private EffectBase startEffect;
    [SerializeField]
    private EffectBase doingEffect;
    [SerializeField]
    private EffectBase endEffect;
    [SerializeField]
    private bool releaseOnFinishEnd = true;
    //private bool autoRelease = true;

    /// <summary>
    /// releaseOnFinishEnd가 false 때만 사용
    /// </summary>
    [SerializeField]
    private UnityEvent onFinishEnd;

    private bool _isPlaying = false;

    private void OnEnable()
    {
        _isPlaying = true;

        bool startEffectExists = startEffect != null;
        if (startEffectExists)
        {
            startEffect.gameObject.SetActive(true);
            startEffect.onStop -= OnFinishStartEffect;
            startEffect.onStop += OnFinishStartEffect;
        }

        if (doingEffect != null)
        {
            doingEffect.gameObject.SetActive(false);
        }

        if (endEffect != null)
        {
            endEffect.gameObject.SetActive(false);
            endEffect.onStop -= OnFinishEndEffect;
        }

        if (!startEffectExists)
        {
            OnFinishStartEffect();
        }
    }

    private void OnDisable()
    {
        _isPlaying = false;
    }

    public override void Stop()
    {
        if (this == null || gameObject == null)
            return;
        if (!gameObject.activeInHierarchy)
            return;
        if (!_isPlaying)
            return;

        _isPlaying = false;
        if (startEffect != null)
        {
            startEffect.gameObject.SetActive(false);
            startEffect.onStop -= OnFinishEndEffect;
        }

        if (doingEffect != null)
        {
            doingEffect.gameObject.SetActive(false);
        }

        if (endEffect != null)
        {
            endEffect.gameObject.SetActive(true);
            endEffect.onStop -= OnFinishEndEffect;
            endEffect.onStop += OnFinishEndEffect;
        }
        else
        {
            OnFinishEndEffect();
        }

        base.Stop();
    }

    private void OnFinishStartEffect()
    {
        if (startEffect != null)
        {
            startEffect.gameObject.SetActive(false);
            startEffect.onStop -= OnFinishEndEffect;
        }

        if (doingEffect != null)
        {
            doingEffect.gameObject.SetActive(true);
        }
        else
        {
            Stop();
        }
    }

    private void OnFinishEndEffect()
    {
        if (releaseOnFinishEnd)
        {
            Release();
        }
        onFinishEnd?.Invoke();
    }
}
