using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using Game.Core;

namespace Combat
{
    public class CharacterManager : MonoBehaviour
    {
        private int _instanceIDSequence;
        private List<CharacterUnit> _characterList;

        private void Awake()
        {
            _characterList = new();
            _instanceIDSequence = 0;
        }

        public List<CharacterUnit> GetCharacterList()
        {
            return _characterList;
        }

        public CharacterUnit CreateCharacter(int characterKey, CombatManager combatManager)
        {
            var characterPrefab = AssetLoader.Instance.GetComponentObject<CharacterUnit>($"Prefabs/Character/Character{characterKey:D2}");
            var characterUnit = Instantiate(characterPrefab);
            
            characterUnit.SetDependency(combatManager);
            characterUnit.SetActionManager(combatManager.ActionManager);
            characterUnit.Initialize(characterKey, _instanceIDSequence++);

            _characterList.Add(characterUnit);
            
            return characterUnit;
        }
    }
}
