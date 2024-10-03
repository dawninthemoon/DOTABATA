using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpriteAnimationEventInfo
{
    public int frameIndex;
    public string eventName;
}

[System.Serializable]
public struct SpriteAnimationClipInfo
{
    public Sprite[] sprites;
    public SpriteAnimationEventInfo[] events;
}

[CreateAssetMenu(fileName = "NewSpriteAnimClip", menuName = "ScriptableObjects/Animation/Clip")]
public class SpriteAnimationClipData : ScriptableObject
{
    public SpriteAnimationClipInfo clipInfo;
}
