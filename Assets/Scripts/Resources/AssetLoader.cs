using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetLoader : MonoBehaviour
{
    public static readonly string DummyBodyPathBase = "Char/Char_Dummy/Combat/C_Body";

    private Dictionary<string, Sprite> _spriteCache;

    public Sprite GetSpriteWithCache(string path)
    {
        if (!_spriteCache.TryGetValue(path, out Sprite output))
        {
            Debug.LogError("Sprite Not Exists!");
        }
        return output;
    }
}
