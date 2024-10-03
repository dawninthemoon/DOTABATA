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
    }

    private void Update()
    {

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
