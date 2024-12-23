using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

namespace Combat
{
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField, Header("Temp")]
        private CharacterUnit characterPrefab;

        private List<CharacterUnit> _characterList;

        private void Awake()
        {
            _characterList = new();
        }

        public List<CharacterUnit> GetCharacterList()
        {
            return _characterList;
        }

        public CharacterUnit CreateCharacter(int characterKey, CombatManager stageManager)
        {
            var characterUnit = Instantiate(characterPrefab);
            
            characterUnit.SetDependency(stageManager);
            characterUnit.SetActionManager(stageManager.ActionManager);
            characterUnit.Initialize(characterKey);

            _characterList.Add(characterUnit);
            
            return characterUnit;
        }
    }
}
