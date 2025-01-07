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
            if (_aiData.detectedTarget != null && !_aiData.detectedTarget.CanTarget())
            {
                _aiData.detectedTarget = null;
            }

            var characterList = characterManager.GetCharacterList();
            if (characterList.Count == 0)
            {
                _aiData.detectedTarget = null;
                return;
            }

            foreach (var target in characterList)
            {
                if (target.CanTarget())
                {
                    _aiData.detectedTarget = target;
                }
            }
        }
    }
}
