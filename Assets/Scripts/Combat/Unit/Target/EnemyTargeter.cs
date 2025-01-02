using System.Collections;
using System.Collections.Generic;
using Combat;
using Unity.Burst.Intrinsics;
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
            if (_currentTarget != null && !_currentTarget.CanTarget())
            {
                _currentTarget = null;
            }

            var characterList = characterManager.GetCharacterList();
            if (characterList.Count == 0)
            {
                _currentTarget = null;
                return;
            }

            foreach (var target in characterList)
            {
                if (target.CanTarget())
                {
                    _currentTarget = target;
                }
            }
        }
    }
}
