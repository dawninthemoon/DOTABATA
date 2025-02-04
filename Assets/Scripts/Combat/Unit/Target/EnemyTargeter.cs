using System.Collections;
using System.Collections.Generic;
using Combat;
using Unity.Burst.Intrinsics;
using UnityEngine;
using Game.StaticData;

namespace Combat
{
    public class EnemyTargeter : MonoBehaviour
    {
        private AIData _aiData;

        public void Initialize(AIData aiData)
        {
            _aiData = aiData;
        }

        public void FindTarget(CombatManager combatManager)
        {
            if (_aiData.detectedTarget != null && !_aiData.detectedTarget.CanTarget())
            {
                _aiData.detectedTarget = null;
            }

            switch (_aiData.targetType)
            {
            case EnemyTargetType.OnlyCharacter:
                FindTarget_OnlyCharacter(combatManager.CharacterManager);
                break;
            case EnemyTargetType.OnlyCore:
                FindTarget_OnlyCore(combatManager.VehicleManager);
                break;
            }
        }

        private void FindTarget_OnlyCharacter(CharacterManager characterManager)
        {
            var characterList = characterManager.GetCharacterList();
            if (characterList.Count == 0)
            {
                _aiData.selectedTarget = _aiData.detectedTarget = null;
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
        
        private void FindTarget_OnlyCore(VehicleManager vehicleManager)
        {
            if (vehicleManager.Core.CanTarget())
            {
                _aiData.detectedTarget = vehicleManager.Core;
            }
            else
            {
                _aiData.selectedTarget = _aiData.detectedTarget = null;
            }
        }
    }
}
