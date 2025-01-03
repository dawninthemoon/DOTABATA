using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using Game.Utils;

namespace Game.Core 
{
    public class AssetLoader : Singleton<AssetLoader> 
    {
        public static readonly string DummyBodyPathBase = "Char/Char_Dummy/Combat/C_Body";
        public static readonly string EnemyPathBase = "Prefabs/Enemy/";

        private Dictionary<string, GameObject> _gameObjectCache = new();
        private Dictionary<string, Sprite> _spriteCache = new();

        public Sprite GetSpriteWithCache(string path)
        {
            if (!_spriteCache.TryGetValue(path, out Sprite output)) 
            {
                output = Resources.Load<Sprite>(path);
                _spriteCache.Add(path, output);
            }
            return output;
        }

        public T GetComponentObject<T>(string path) where T : Component
        {
            if (!_gameObjectCache.TryGetValue(path, out GameObject output)) 
            {
                output = Resources.Load<GameObject>(path);
                _gameObjectCache.Add(path, output);
            }
            return output?.GetComponent<T>();
        }
    }
}
