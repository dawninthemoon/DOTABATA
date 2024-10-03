using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpriteAnimatorInfo
{
    public string animationName;
    public SpriteAnimationClipData clip;
}

[CreateAssetMenu(fileName = "NewSpriteAnimator", menuName = "ScriptableObjects/Animation/Animator")]
public class SpriteAnimatorData : ScriptableObject
{
    public string defaultAnimationName;
    public SpriteAnimatorInfo[] data;
}
