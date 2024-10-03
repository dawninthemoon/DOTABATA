using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private SpriteAnimatorData animatorData;

    private string _currentAnimationName;
    private int _spriteIndex;
    private float _counter;

    public SpriteRenderer Renderer => spriteRenderer;
    public string CurrentAnimationName => _currentAnimationName;
    
    private void OnEnable()
    {
        if (spriteRenderer is null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        StartAnimation();
    }

    private void StartAnimation()
    {
        _currentAnimationName = animatorData.defaultAnimationName;
        _spriteIndex = 0;
    }

    private void Update()
    {
        var clipInfo = FindClipInfo(_currentAnimationName);
        if (clipInfo is null)
        {
            return;
        }

        _counter += Time.deltaTime;
        if (_counter < 1f / clipInfo.clip.framePerSec)
        {
            return;
        }

        _counter -= 1f / clipInfo.clip.framePerSec;

        if (_spriteIndex >= clipInfo.clip.sprites.Length)
        {
            _spriteIndex = 0;
        }

        var sprite = clipInfo.clip.sprites[_spriteIndex];
        spriteRenderer.sprite = sprite;
    }

    public void ChangeAnimation(string animationName, bool resetIndex = true)
    {
        _currentAnimationName = animationName;
        _counter = 999f;
        if (resetIndex)
        {
            _spriteIndex = 0;
        }
    }

    public void SetEndCallback(string animationName, Action callback)
    {

    }

    public void SetEventCallback(string animationName, string eventName, Action callback)
    {
        
    }

    private SpriteAnimatorInfo FindClipInfo(string animationName)
    {
        var clipData = animatorData.data.Where(x => x.animationName.Equals(animationName)).SingleOrDefault();
        return clipData;
    }
}
