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

    private SpriteAnimatorInfo _clipInfo;
    private Dictionary<string, Action> _endCallbackDictionary;

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

    private void Awake()
    {
        _endCallbackDictionary = new();
    }

    private void StartAnimation()
    {
        _currentAnimationName = animatorData.defaultAnimationName;
        _spriteIndex = 0;
    }

    private void Update()
    {
        if (_clipInfo is null)
        {
            return;
        }

        _counter += Time.deltaTime;
        if (_counter < 1f / _clipInfo.clip.framePerSec)
        {
            return;
        }

        _counter = 0f;

        if (_spriteIndex >= _clipInfo.clip.sprites.Length)
        {
            if (_clipInfo.clip.loop)
            {
                _spriteIndex = 0;
            }
            else
            {
                OnAnimationEnd();
                return;
            }
        }

        var sprite = _clipInfo.clip.sprites[_spriteIndex];
        spriteRenderer.sprite = sprite;

        ++_spriteIndex;
    }

    public void ChangeAnimation(string animationName, bool resetIndex = true)
    {
        if (_currentAnimationName.Equals(animationName))
        {
            return;
        }
        
        var clipInfo = FindClipInfo(animationName);
        if (clipInfo is null)
        {
            return;
        }
        _clipInfo = clipInfo;

        _currentAnimationName = animationName;
        _counter = 999f;
        if (resetIndex)
        {
            _spriteIndex = 0;
        }
    }

    public void SetEndCallback(string animationName, Action callback)
    {
        if (!_endCallbackDictionary.ContainsKey(animationName))
        {
            _endCallbackDictionary.Add(animationName, callback);
        }
        else
        {
            _endCallbackDictionary[animationName] = callback;
        }
    }

    public void SetEventCallback(string eventName, Action callback)
    {
        
    }

    private void OnAnimationEnd()
    {
        if (_endCallbackDictionary.TryGetValue(_currentAnimationName, out Action callback))
        {
            callback?.Invoke();
        }
    }

    private SpriteAnimatorInfo FindClipInfo(string animationName)
    {
        SpriteAnimatorInfo clipData = null;
        foreach (var data in animatorData.data)
        {
            if (data.animationName.Equals(animationName))
            {
                clipData = data;
                break;
            }
        }
        return clipData;
    }
}
