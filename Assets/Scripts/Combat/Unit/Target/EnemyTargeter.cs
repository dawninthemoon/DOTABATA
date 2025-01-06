using System.Collections;
using System.Collections.Generic;
using Combat;
using Unity.Burst.Intrinsics;
using UnityEngine;

namespace Combat
{
    public class EnemyTargeter : MonoBehaviour
    {
        private AIData _aiData;

        public void Initialize(AIData aiData)
        {
            _aiData = aiData;
        }

        public void FindTarget(CharacterManager characterManager)
        {
            if (_aiData.currentTarget != null && !_aiData.currentTarget.CanTarget())
            {
                _aiData.currentTarget = null;
            }

            var characterList = characterManager.GetCharacterList();
            if (characterList.Count == 0)
            {
                _aiData.currentTarget = null;
                return;
            }

            foreach (var target in characterList)
            {
                if (target.CanTarget())
                {
                    _aiData.currentTarget = target;
                }
            }
        }
    }
}
