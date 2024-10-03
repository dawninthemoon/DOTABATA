using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpriteAnimationEventInfo
{
    public int frameIndex;
    public string eventName;
}

[CreateAssetMenu(fileName = "NewSpriteAnimClip", menuName = "ScriptableObjects/Animation/Clip")]
public class SpriteAnimationClipData : ScriptableObject
{
    public int framePerSec = 10;
    public bool loop;
    public Sprite[] sprites;
    public SpriteAnimationEventInfo[] events;
}
