using System.Collections;
using System.Collections.Generic;
using Combat;
using UnityEngine;

namespace Combat
{
    public class EnemyTargeter : TargeterBase
    {
        private float _detectRange;

        public void Initialize(float detectRange)
        {
            Reset();
            _detectRange = detectRange;
        }

        public void FindTarget(CharacterManager characterManager)
        {
            var characterList = characterManager.GetCharacterList();
            if (characterList.Count == 0)
            {
                _currentTarget = null;
                return;
            }

            _currentTarget = characterList[0];
        }
    }
}
