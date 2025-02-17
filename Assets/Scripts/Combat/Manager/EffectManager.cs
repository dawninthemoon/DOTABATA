using System.Collections;
using System.Collections.Generic;
using Game.Core;
using UnityEngine;

namespace Combat
{
    public class EffectManager : MonoBehaviour
    {
        private Dictionary<string, EffectBase> _effectPrefabDictionary;

        public void Initialize()
        {
            
        }

        public EffectBase CreateEffect(string effectName, Vector3 pos)
        {
            string path = $"Prefabs/Effects/{effectName}";
            var instance = CreateEffectInstance(path);
            instance.transform.position = pos;
            return instance;
        }

        public EffectBase CreateEffectWithAttach(string effectName, IEffectAttachable attachable)
        {
            var instance = CreateEffectInstance(effectName);
            instance.transform.SetParent(attachable.EffectRoot);
            instance.transform.localPosition = Vector3.zero;
            return instance;
        }

        private EffectBase CreateEffectInstance(string effectName)
        {
            EffectBase prefab = AssetLoader.Instance.GetComponentObject<EffectBase>(effectName);
            if (prefab == null)
            {
                Debug.Log($"Effect {effectName} not Exists");
                return null;
            }

            EffectBase instance = Instantiate(prefab);
            return instance;
        }
    }
}
